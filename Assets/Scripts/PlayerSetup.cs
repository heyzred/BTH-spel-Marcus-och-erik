using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

     void Start()
    {
        if (!isLocalPlayer)
        {
            DisabledComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }


        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();


        GameManager.RegisterPlayer(_netID, _player);
    }




    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);

    }


    void DisabledComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    // when we are destroyed
     void OnDisable()
    {
        // re-enable the scene camera
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        GameManager.UnRegisterPlayer(transform.name);
    }







}
