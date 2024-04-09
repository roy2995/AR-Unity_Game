using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ZXing;
using ZXing.QrCode;


public class QRcodeGenerator : MonoBehaviour
{
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

    [Button("Encode")]
    public void OnClickEncode()
    {
        storedEncodedTexture = new Texture2D(256, 256);
        EncodeTextToQR();
    }

    private void EncodeTextToQR()
    {
        if (storedEncodedTexture == null)
        {
            Debug.LogError("String value it's null");
            return;
        }

        Color32[] pixelTexture = Encode(qrString, storedEncodedTexture.width, storedEncodedTexture.height);
        storedEncodedTexture.SetPixels32(pixelTexture);
        storedEncodedTexture.Apply();

        SaveToImage.SaveTexture2DToFile(storedEncodedTexture, Application.dataPath + "/Resources/QR Images/" + qrString, SaveToImage.SaveTextureFileFormat.PNG);
    }

}
