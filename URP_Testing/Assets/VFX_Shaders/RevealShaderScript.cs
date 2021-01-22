using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RevealShaderScript : MonoBehaviour
{
	public Transform m_objectToTrack = null;
	private Material m_materialRef = null;
	private Renderer m_renderer = null;
    

	private float min = 3f;
	private float max = 6f;
	public string state = "Off";
	private float count = 0f;

    public Renderer Renderer{
    	get{
    		if(m_renderer == null)
    			m_renderer = this.GetComponent<Renderer>();
			return m_renderer;
    	}
    }

    public Material MaterialRef
    {
    	get
    	{
    		if(m_materialRef == null)
    			m_materialRef = Renderer.material;
			return m_materialRef;
    	}
    }

	private void Awake(){
    	m_renderer = this.GetComponent<Renderer>();
    	m_materialRef = m_renderer.material;
    }

    private void Update()
    {
    	if (m_objectToTrack != null)
    	{
    		Debug.Log(m_objectToTrack);
    		MaterialRef.SetVector("_Vector3_Position", m_objectToTrack.position);
    	}

    	if (Input.GetKeyDown("f") && state == "Off") state = "Off-On";

    	if (state == "Off-On") {
    		count += Time.fixedDeltaTime/10;
    		if (count >= 1) {
    			state = "On";
    			count = 0;
    		}
    	}
    	if (state == "On") {
    		count += Time.fixedDeltaTime/2;
    		if (count >= 1) {
    			state = "On-Off";
    			count = 1;
    		}
    	}
    	if (state == "On-Off") {
    		count -= Time.fixedDeltaTime;
    		if (count <= 0) {
    			state = "Off";
    			count = 0;
    		}
    	}

    	if (state != "On") MaterialRef.SetFloat("Vector1_Distance", (min + min*count));
    	else MaterialRef.SetFloat("Vector1_Distance", (min + min));
    }

    private void OnDestory(){
    	m_renderer = null;
    	if (m_materialRef != null)
    		Destroy(m_materialRef);
		m_materialRef = null;
    }

}
