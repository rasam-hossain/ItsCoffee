﻿using System;
using System.Data;
using Dapper;

namespace ItsCoffee.Core
{
    /*
     * Note: The purpose of this file is to map Guids to strings for Dapper to be able to use them
     */
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string) value);
        }
    }
}
