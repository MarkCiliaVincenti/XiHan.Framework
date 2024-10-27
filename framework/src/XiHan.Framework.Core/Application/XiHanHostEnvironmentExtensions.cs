﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:XiHanHostEnvironmentExtensions
// Guid:099e1481-cd66-4c3b-8daa-992c25892bbf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/10/28 4:16:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Hosting;
using XiHan.Framework.Utils.System;

namespace XiHan.Framework.Core.Application;

/// <summary>
/// 曦寒宿主环境扩展方法
/// </summary>
public static class XiHanHostEnvironmentExtensions
{
    /// <summary>
    /// 是否为开发环境
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns></returns>
    public static bool IsDevelopment(this IXiHanHostEnvironment hostEnvironment)
    {
        CheckHelper.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Development);
    }

    /// <summary>
    /// 是否为测试环境
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns></returns>
    public static bool IsStaging(this IXiHanHostEnvironment hostEnvironment)
    {
        CheckHelper.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Staging);
    }

    /// <summary>
    /// 是否为生产环境
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <returns></returns>
    public static bool IsProduction(this IXiHanHostEnvironment hostEnvironment)
    {
        CheckHelper.NotNull(hostEnvironment, nameof(hostEnvironment));

        return hostEnvironment.IsEnvironment(Environments.Production);
    }

    /// <summary>
    /// 是否为指定环境
    /// </summary>
    /// <param name="hostEnvironment"></param>
    /// <param name="environmentName"></param>
    /// <returns></returns>
    public static bool IsEnvironment(this IXiHanHostEnvironment hostEnvironment, string environmentName)
    {
        CheckHelper.NotNull(hostEnvironment, nameof(hostEnvironment));

        return string.Equals(hostEnvironment.EnvironmentName, environmentName, StringComparison.OrdinalIgnoreCase);
    }
}