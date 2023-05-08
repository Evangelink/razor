﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Razor.PooledObjects;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Razor.ProjectEngineHost.Serialization;

internal delegate void WriteProperties<T>(JsonDataWriter writer, T value);
internal delegate void WriteValue<T>(JsonDataWriter writer, T value);

internal partial class JsonDataWriter
{
    private static readonly ObjectPool<JsonDataWriter> s_pool = DefaultPool.Create(Policy.Instance);

    public static JsonDataWriter Get(JsonWriter writer)
    {
        var dataWriter = s_pool.Get();
        dataWriter._writer = writer;

        return dataWriter;
    }

    public static void Return(JsonDataWriter dataWriter)
        => s_pool.Return(dataWriter);

    [AllowNull]
    private JsonWriter _writer;

    private JsonDataWriter()
    {
    }

    public void Write(string propertyName, bool value)
    {
        _writer.WritePropertyName(propertyName);
        _writer.WriteValue(value);
    }

    public void WriteIfNotTrue(string propertyName, bool value)
    {
        if (value)
        {
            Write(propertyName, value);
        }
    }

    public void WriteIfNotFalse(string propertyName, bool value)
    {
        if (!value)
        {
            Write(propertyName, value);
        }
    }

    public void Write(string propertyName, int value)
    {
        _writer.WritePropertyName(propertyName);
        _writer.WriteValue(value);
    }

    public void WriteIfNotDefault(string propertyName, int value, int defaultValue = default)
    {
        if (value != defaultValue)
        {
            Write(propertyName, value);
        }
    }

    public void Write(string propertyName, string? value)
    {
        _writer.WritePropertyName(propertyName);
        _writer.WriteValue(value);
    }

    public void WriteIfNotDefault(string propertyName, string? value, string? defaultValue)
    {
        if (value != defaultValue)
        {
            Write(propertyName, value);
        }
    }

    public void WriteIfNotNull(string propertyName, string? value)
    {
        if (value is not null)
        {
            Write(propertyName, value);
        }
    }

    public void Write(string propertyName, Uri? value)
    {
        _writer.WritePropertyName(propertyName);
        Write(value);
    }

    public void Write(Uri? value)
    {
        if (value is null)
        {
            _writer.WriteNull();
        }
        else
        {
            _writer.WriteValue(value.AbsoluteUri);
        }
    }

    public void WriteObject<T>(string propertyName, T? value, WriteProperties<T> writeProperties)
    {
        _writer.WritePropertyName(propertyName);
        WriteObject(value, writeProperties);
    }

    public void WriteObject<T>(T? value, WriteProperties<T> writeProperties)
    {
        if (value is null)
        {
            _writer.WriteNull();
            return;
        }

        _writer.WriteStartObject();
        writeProperties(this, value);
        _writer.WriteEndObject();
    }

    public void WriteArray<T>(IEnumerable<T>? elements, WriteValue<T> writeElement)
    {
        if (writeElement is null)
        {
            throw new ArgumentNullException(nameof(writeElement));
        }

        if (elements is null)
        {
            _writer.WriteNull();
            return;
        }

        _writer.WriteStartArray();

        foreach (var element in elements)
        {
            writeElement(this, element);
        }

        _writer.WriteEndArray();
    }

    public void WriteArray<T>(string propertyName, IEnumerable<T>? elements, WriteValue<T> writeElement)
    {
        _writer.WritePropertyName(propertyName);
        WriteArray(elements, writeElement);
    }

    public void WriteArray<T>(IReadOnlyList<T>? elements, WriteValue<T> writeElement)
    {
        if (writeElement is null)
        {
            throw new ArgumentNullException(nameof(writeElement));
        }

        if (elements is null)
        {
            _writer.WriteNull();
            return;
        }

        _writer.WriteStartArray();

        var count = elements.Count;

        for (var i = 0; i < count; i++)
        {
            writeElement(this, elements[i]);
        }

        _writer.WriteEndArray();
    }

    public void WriteArray<T>(string propertyName, IReadOnlyList<T>? elements, WriteValue<T> writeElement)
    {
        _writer.WritePropertyName(propertyName);
        WriteArray(elements, writeElement);
    }

    public void WriteArray<T>(ImmutableArray<T> elements, WriteValue<T> writeElement)
    {
        if (writeElement is null)
        {
            throw new ArgumentNullException(nameof(writeElement));
        }

        _writer.WriteStartArray();

        foreach (var element in elements)
        {
            writeElement(this, element);
        }

        _writer.WriteEndArray();
    }

    public void WriteArray<T>(string propertyName, ImmutableArray<T> elements, WriteValue<T> writeElement)
    {
        _writer.WritePropertyName(propertyName);
        WriteArray(elements, writeElement);
    }

    public void WriteArrayIfNotNullOrEmpty<T>(string propertyName, IEnumerable<T>? elements, WriteValue<T> writeElement)
    {
        if (elements?.Any() == true)
        {
            WriteArray(propertyName, elements, writeElement);
        }
    }

    public void WriteArrayIfNotNullOrEmpty<T>(string propertyName, IReadOnlyList<T>? elements, WriteValue<T> writeElement)
    {
        if (elements is { Count: > 0 })
        {
            WriteArray(propertyName, elements, writeElement);
        }
    }

    public void WriteArrayIfNotDefaultOrEmpty<T>(string propertyName, ImmutableArray<T> elements, WriteValue<T> writeElement)
    {
        if (!elements.IsDefaultOrEmpty)
        {
            WriteArray(propertyName, elements, writeElement);
        }
    }
}
