﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
// FileName:AesHelper
// Guid:2494125d-816b-41f8-ba8e-7eadfa890095
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/10/11 5:25:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Framework.Utils.Security.Cryptography;

/// <summary>
/// Aes 加密解密
/// </summary>
/// <remarks>
/// 是一种对称密钥加密算法，广泛用于数据加密和保护。
/// </remarks>
public static class AesHelper
{
    // AES KEY 的位数
    private const int KeySize = 256;

    // 加密块大小
    private const int BlockSize = 128;

    // 迭代次数
    private const int Iterations = 10000;

    /// <summary>
    /// 加密方法
    /// </summary>
    /// <param name="plainText">要加密的文本</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public static string Encrypt(string plainText, string password)
    {
        // 生成盐
        byte[] salt = new byte[BlockSize / 8];
        byte[] key = DeriveKey(password, salt, KeySize / 8);
        byte[] iv = DeriveKey(password, salt, BlockSize / 8);

        // 返回加密结果
        return Encrypt(plainText, key, iv);
    }

    /// <summary>
    /// 自定义 Key 和 IV 的加密方法
    /// </summary>
    /// <param name="plainText">要加密的文本</param>
    /// <param name="key">自定义的 Key</param>
    /// <param name="iv">自定义的 IV</param>
    /// <returns></returns>
    public static string Encrypt(string plainText, string key, string iv)
    {
        byte[] keyByte = Convert.FromBase64String(key);
        byte[] ivByte = Convert.FromBase64String(iv);
        return Encrypt(plainText, keyByte, ivByte);
    }

    /// <summary>
    /// 自定义 Key 和 IV 的加密方法
    /// </summary>
    /// <param name="plainText">要加密的文本</param>
    /// <param name="key">自定义的 Key</param>
    /// <param name="iv">自定义的 IV</param>
    /// <returns></returns>
    public static string Encrypt(string plainText, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // 加密算法
        string cipherText;
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        cs.Write(plainBytes, 0, plainBytes.Length);
        cs.FlushFinalBlock();
        byte[] cipherBytes = ms.ToArray();
        cipherText = Convert.ToBase64String(cipherBytes);

        // 返回加密结果
        return cipherText;
    }

    /// <summary>
    /// 解密方法
    /// </summary>
    /// <param name="cipherText">要解密的文本</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Decrypt(string cipherText, string password)
    {
        // 生成盐
        byte[] salt = new byte[BlockSize / 8];
        byte[] key = DeriveKey(password, salt, KeySize / 8);
        byte[] iv = DeriveKey(password, salt, BlockSize / 8);

        return Decrypt(cipherText, key, iv);
    }

    /// <summary>
    /// 自定义 Key 和 IV 的解密方法
    /// </summary>
    /// <param name="cipherText">要解密的文本</param>
    /// <param name="key">自定义的 Key</param>
    /// <param name="iv">自定义的 IV</param>
    /// <returns></returns>
    public static string Decrypt(string cipherText, string key, string iv)
    {
        byte[] keyByte = Convert.FromBase64String(key);
        byte[] ivByte = Convert.FromBase64String(iv);
        return Decrypt(cipherText, keyByte, ivByte);
    }

    /// <summary>
    /// 自定义 Key 和 IV 的解密方法
    /// </summary>
    /// <param name="cipherText">要解密的文本</param>
    /// <param name="key">自定义的 Key</param>
    /// <param name="iv">自定义的 IV</param>
    /// <returns></returns>
    public static string Decrypt(string cipherText, byte[] key, byte[] iv)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // 解密算法
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(cipherBytes, 0, cipherBytes.Length);
        cs.FlushFinalBlock();
        byte[] plainBytes = ms.ToArray();
        string plainText = Encoding.UTF8.GetString(plainBytes);

        // 返回解密结果
        return plainText;
    }

    /// <summary>
    /// 派生密钥
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private static byte[] DeriveKey(string password, byte[] salt, int bytes)
    {
        using Rfc2898DeriveBytes pbkdf2 = new(password, salt, Iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(bytes);
    }
}
