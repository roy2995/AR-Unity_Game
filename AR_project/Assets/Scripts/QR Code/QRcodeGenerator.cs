using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;


public class QRcodeGenerator : MonoBehaviour
{
    [Header("QR Code Generator")]
    [SerializeField] private GameObject qrPrefab;
    [SerializeField] private string qrString;

    [Header("QR Code Settings")]
    [SerializeField] private RawImage imageReceiver;
    private Texture2D storedEncodedTexture;

    private void Start()
    {
        storedEncodedTexture = new Texture2D(256, 256);
        Debug.Log(Application.dataPath);
    }

    private Color32[] Encode(string textToEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textToEncoding);
    }

    public void OnClickEncode()
    {
        EncodeTextToQR();
    }

    private void EncodeTextToQR()
    {
        if (storedEncodedTexture == null)
        {
            Debug.LogError("String value it's null");
            // string textWrite = string.IsNullOrEmpty(qrString)? "Null" : qrString;
        }

        Color32[] pixelTexture = Encode(qrString, storedEncodedTexture.width, storedEncodedTexture.height);
        storedEncodedTexture.SetPixels32(pixelTexture);
        storedEncodedTexture.Apply();

        imageReceiver.texture = storedEncodedTexture;
        SaveToImage.SaveTexture2DToFile(storedEncodedTexture, Application.dataPath + "/Resources/QR Images/" + qrString, SaveToImage.SaveTextureFileFormat.PNG);
    }

}
