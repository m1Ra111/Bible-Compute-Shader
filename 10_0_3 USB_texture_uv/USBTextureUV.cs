using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBTextureUV : MonoBehaviour
{
    public ComputeShader m_shader;
    public Texture m_tex;
    private RenderTexture m_mainTex;    

    int m_texSize = 256;
    Renderer m_rend;  

    // Start is called before the first frame update
    void Start()
    {
        // first, we create the texture
        m_mainTex = new RenderTexture(m_texSize, m_texSize, 0, RenderTextureFormat.ARGB32);
        m_mainTex.enableRandomWrite = true;
        m_mainTex.Create();
    }

    // Update is called once per frame
    void Update()
    {
        // then we get the mesh renderer
        m_rend = GetComponent<Renderer>();
        m_rend.enabled = true;

        // we send the values to the compute shader
        m_shader.SetTexture(0, "Result", m_mainTex);
        m_shader.SetTexture(0, "ColTex", m_tex);
        m_rend.material.SetTexture("_MainMap", m_mainTex);

        // finally, we dispatch the threads
        m_shader.Dispatch(0, m_texSize/8, m_texSize/8, 1);
    }
}
