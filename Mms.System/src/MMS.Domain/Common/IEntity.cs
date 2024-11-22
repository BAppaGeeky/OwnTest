// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MMS.Domain.Common;

public interface IEntity
{
}

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}