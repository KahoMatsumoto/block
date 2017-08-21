using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		OSCHandler.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKey(KeyCode.R)){
		//	SceneManager.LoadScene ("Right");
		//}
		//if(Input.GetKey(KeyCode.L)){
		//	SceneManager.LoadScene ("Left");
		//}
        ListenToOSC();
	}
	void ListenToOSC()
	{
		OSCHandler.Instance.UpdateLogs();
		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
		servers = OSCHandler.Instance.Servers;

		foreach (KeyValuePair<string, ServerLog> item in servers)
		{
			//Debug.Log(item.Value.log.Count);
			if (item.Value.log.Count > 0)
			{
				Debug.Log("count is more than zero");
				int lastPacketIndex = item.Value.packets.Count - 1;

				string s = item.Value.packets[lastPacketIndex].Data[0].ToString();
				if (item.Value.packets[lastPacketIndex].Address == "/scene")
				{
                    switch (int.Parse(s))
                    {
                        case 0:
                            SceneManager.LoadScene("Left");
                            break;
                        case 1:
                            SceneManager.LoadScene("Right");
                            break;
                        default:
                            SceneManager.LoadScene("LeftMany");
                            break;
                    }
					//SceneManager.LoadScene(s);
				}
			}
		}
		OSCHandler.Instance.UpdateLogs();
	}

}
