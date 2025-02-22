﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// https://blog.csdn.net/weixin_30411239/article/details/97632834
/// </summary>
namespace Nolan.Application.Shared
{
     
    public sealed class AppSrvResult
    {
        public AppSrvResult()
        {
        }

        public AppSrvResult([NotNull] ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public bool IsSuccess
        {
            get
            {
                return ProblemDetails == null;
            }
        }

        public ProblemDetails ProblemDetails { get; set; }

        public static implicit operator AppSrvResult([NotNull] ProblemDetails problemDetails)
        {
            return new AppSrvResult
            {
                ProblemDetails = problemDetails
            };
        }
    }

    /// <summary>
    /// Application返回结果包装类,有返回类型
    /// </summary>
    [Serializable]
    public sealed class AppSrvResult<TValue>
    {
        public AppSrvResult()
        {
        }

        public AppSrvResult([NotNull] TValue value)
        {
            Content = value;
        }

        public AppSrvResult([NotNull] ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public bool IsSuccess
        {
            get
            {
                return ProblemDetails == null && Content != null;
            }
        }

        public TValue Content { get; set; }

        public ProblemDetails ProblemDetails { get; set; }

        public static implicit operator AppSrvResult<TValue>(AppSrvResult result)
        {
            return new AppSrvResult<TValue>
            {
                Content = default
                ,
                ProblemDetails = result.ProblemDetails
            };
        }

        public static implicit operator AppSrvResult<TValue>(ProblemDetails problemDetails)
        {
            return new AppSrvResult<TValue>
            {
                Content = default
                ,
                ProblemDetails = problemDetails
            };
        }

        public static implicit operator AppSrvResult<TValue>(TValue value)
        {
            return new AppSrvResult<TValue>(value);
        }
    }
}
