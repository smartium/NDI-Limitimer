using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using OscJack;

class OscReceiver : MonoBehaviour
{
    public Text txtTimer;
    
    IEnumerator Start()
    {
        var server = new OscServer(7000); // Port number

        server.MessageDispatcher.AddCallback(
            "/addminute", // OSC address
            (string address, OscDataHandle data) =>
            {
                txtTimer.text = "test";
                print(txtTimer.text);
            }
        );
        
        server.MessageDispatcher.AddCallback(
            "/subminute", // OSC address
            (string address, OscDataHandle data) => {
                Debug.Log(string.Format("({0}, {1})",
                    data.GetElementAsFloat(0),
                    data.GetElementAsFloat(1)));
            }
        );

        //yield return new WaitForSeconds(10);
        yield break;
        //server.Dispose();
    }
}
