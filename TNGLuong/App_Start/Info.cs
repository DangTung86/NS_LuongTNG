using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

public class Info
{
    public Info()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool IsNumber(string pText)
    {
        Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
        return regex.IsMatch(pText);
    }
    public string encryptString(string str)
    {
        MD5CryptoServiceProvider MD5Code = new MD5CryptoServiceProvider();
        byte[] strByte = Encoding.UTF8.GetBytes(str);
        strByte = MD5Code.ComputeHash(strByte);
        StringBuilder sb = new StringBuilder();
        foreach (byte ba in strByte)
        {
            sb.Append(ba.ToString("x2").ToLower());
        }
        return sb.ToString();
    }
}