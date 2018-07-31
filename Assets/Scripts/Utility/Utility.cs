using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Utility {

    /// <summary>返回待加密字符串的MD5码 </summary>
    public static string GetMd5(string str) {
        string strMd5Code = "";
        MD5 md5 = MD5.Create();
        byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        for (int i = 0; i < bytes.Length; i++) {
            strMd5Code += bytes[i].ToString("x2");
        }
        return strMd5Code;
    }

    /// <summary>将Unix时间戳转化为时间 </summary>
    public static DateTime UnixStampToDateTime(this int timeStamp) {
        DateTime oriTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return oriTime.AddSeconds(timeStamp);
    }

    /// <summary>将时间转化为Unix时间戳 </summary>
    public static int DateTimeToUnixStamp(this DateTime time) {
        DateTime oriTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (int)(time - oriTime).TotalSeconds;
    }

    private const string encryptKey = "GameGame";
#if !UNITY_EDITOR
        private static byte[] encryptKeyBytes = Encoding.UTF8.GetBytes(encryptKey);
#endif

    /// <summary>DES加密字符串 </summary>
    public static string Encrypt(string str) {
#if UNITY_EDITOR
        byte[] encryptKeyBytes = Encoding.UTF8.GetBytes(encryptKey);
#endif
        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象
        byte[] data = Encoding.UTF8.GetBytes(str);//定义字节数组，用来存储要加密的字符串  
        MemoryStream MStream = new MemoryStream();//实例化内存流对象      
        CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(encryptKeyBytes, encryptKeyBytes), CryptoStreamMode.Write);
        CStream.Write(data, 0, data.Length);//向加密流中写入数据      
        CStream.FlushFinalBlock();//释放加密流      
        return Convert.ToBase64String(MStream.ToArray());
    }

    /// <summary>DES解密字符串 </summary>
    public static string Decrypt(string str) {
#if UNITY_EDITOR
        byte[] encryptKeyBytes = Encoding.UTF8.GetBytes(encryptKey);
#endif
        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
        byte[] data = Convert.FromBase64String(str);
        MemoryStream MStream = new MemoryStream();
        CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(encryptKeyBytes, encryptKeyBytes), CryptoStreamMode.Write);
        CStream.Write(data, 0, data.Length);
        CStream.FlushFinalBlock();
        return Encoding.UTF8.GetString(MStream.ToArray());
    }

    public async static Task WriteAsync(string folderPath, string fileName, string content) {
        string fullPath = folderPath + "/" + fileName;
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }
        using (StreamWriter sw = new StreamWriter(fullPath, false, Encoding.UTF8)) {//覆盖该文件
            await sw.WriteAsync(content);
        }
    }

    public async static Task<string> ReadAsync(string folderPath, string fileName) {
        string fullPath = folderPath + "/" + fileName;
        using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read)) {
            using (StreamReader sw = new StreamReader(fs, Encoding.UTF8)) {
                return await sw.ReadToEndAsync();
            }
        }
    }

    public async static Task WriteInPresidentAsync(string folderPathInPresident, string fileName, string content) {
        await WriteAsync(Application.persistentDataPath + folderPathInPresident, fileName, content);
    }

    public async static Task<string> ReadInPresidentAsync(string folderPathInPresident, string fileName) {
        return await ReadAsync(Application.persistentDataPath + folderPathInPresident, fileName);
    }

    public static void Exchange<T>(ref T t1, ref T t2) {
        T temp = t1;
        t1 = t2;
        t2 = temp;
    }

}
