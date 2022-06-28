﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.WebApi.Shared.ResultModel
{
    /// <summary>
    /// 返回Code
    /// </summary>
    public enum RCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [JsonProperty("1000")]
        Success = 1000,

        /// <summary>
        /// 登录超时,需重新登录
        /// </summary>
        [JsonProperty("2000")]
        NeedLogin = 2000,

        /// <summary>
        /// 程序异常
        /// </summary>
        [JsonProperty("3000")]
        Exception = 3000,

        /// <summary>
        /// 系统错误
        /// </summary>
        [JsonProperty("4000")]
        SysError = 4000
    }
    /// <summary>
    /// 返回结果对象
    /// ReturnObject Robj
    /// 默认RCode为成功,Message为成功.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Robj<T>
    {
        T result = default(T);
        RCode code = RCode.Success;
        string message = "操作成功";

        /// <summary>
        /// 结果
        /// </summary>
        public T Result
        {
            get { return result; }
            set { result = value; }
        }
        /// <summary>
        /// 执行结果
        /// </summary>
        public RCode Code
        {
            get { return code; }
            set { code = value; }
        }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message
        { get { return message; } set { message = value; } }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="result">返回结果</param>
        /// <param name="msg">提示消息</param>
        public void Success(T result, string msg = "操作成功")
        {
            this.code = RCode.Success;
            this.result = result;
            this.Message = msg;
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <param name="code"></param>
        public void Error(string msg, RCode code = RCode.Exception)
        {
            this.code = code;
            this.Message = msg;
        }
    }
}
