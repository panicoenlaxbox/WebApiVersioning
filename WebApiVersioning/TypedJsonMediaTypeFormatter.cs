﻿using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace WebApiVersioning
{
    class TypedJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private readonly Type _resourceType;

        public TypedJsonMediaTypeFormatter(Type resourceType, MediaTypeHeaderValue mediaType)
        {
            _resourceType = resourceType;
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(mediaType);
        }

        public override bool CanReadType(Type type)
        {
            return _resourceType == type || _resourceType == type.GetTypeFromIEnumerable();
        }

        public override bool CanWriteType(Type type)
        {
            return _resourceType == type || _resourceType == type.GetTypeFromIEnumerable();
        }
    }
}