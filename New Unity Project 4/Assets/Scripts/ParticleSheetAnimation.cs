using UnityEngine;
using System.Collections;

[AddComponentMenu("VStar/Effect/ParticleSheetAnimation")]
public class ParticleSheetAnimation : MonoBehaviour
{
	public int tileX = 1;
	public int tileY = 1;
	public float startEmitTime = 0.0f;
	private bool m_bEmitted = false;
	private float m_oneDivTileX = 0.0f;
	private float m_oneDivTileY = 0.0f;
	private GameObject m_gameObject;
	private ParticleSystem m_particleSystem;
	private Material m_material;
	private float m_elaspe = 0.0f;
	private float m_changeTime = 0.0f;
	private float m_duration = 0.0f;
	private float m_lifeTime = 0.0f;
	private float m_compensationTime = -1.0f;
	private bool m_bDO = false;
	private int m_curX = 0;
	private int m_curY = 0;

	void OnDestroy()
	{
		if (m_material)
		{
			DestroyImmediate(m_material);
			m_material = null;
		}
	}

	// Use this for initialization
	void Start()
	{
		if (tileX <= 1 && tileY <= 1)
		{
			return;
		}

		m_oneDivTileX = 1.0f / tileX;
		m_oneDivTileY = 1.0f / tileY;

		m_gameObject = gameObject;
		if (m_gameObject)
		{
			m_particleSystem = m_gameObject.GetComponent<ParticleSystem>();
			if (m_particleSystem)
			{
				Renderer renderer = m_particleSystem.renderer;
				if (renderer)
				{
					m_material = renderer.material;
					if (m_material)
					{
						m_duration = m_particleSystem.duration;
						m_lifeTime = m_particleSystem.startLifetime;
						m_changeTime = m_lifeTime / (tileX * tileY);
						m_bDO = true;
						m_material.mainTextureScale = new Vector2(m_oneDivTileX, m_oneDivTileY);
						m_elaspe = m_particleSystem.time;
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!m_bDO)
		{
			return;
		}

		float nowTime = m_particleSystem.time;
		if (!m_bEmitted && nowTime >= startEmitTime)
		{
			m_bEmitted = true;
			m_compensationTime = nowTime - startEmitTime;
			m_material.mainTextureOffset = new Vector2(m_curX * m_oneDivTileX, (tileY - 1) * m_oneDivTileY);
		}

		if (nowTime > startEmitTime + m_lifeTime && nowTime <= m_duration)
		{
			m_bEmitted = false;
			m_compensationTime = m_duration - nowTime;
			m_elaspe = 0.0f;
			return;
		}

		if (!m_bEmitted)
		{
			return;
		}

		if (m_compensationTime > 0.0f)
		{
			m_elaspe += m_compensationTime;
			m_compensationTime = -1.0f;
		}

		float deltaTime = Time.deltaTime;
		m_elaspe += deltaTime;
		if (m_elaspe >= m_changeTime)
		{
			m_elaspe -= m_changeTime;
		}
		else
		{
			return;
		}

		m_curX++;
		if (m_curX >= tileX)
		{
			m_curX = 0;
			m_curY++;
			if (m_curY >= tileY)
			{
				m_curY = 0;
			}
		}
		m_material.mainTextureOffset = new Vector2(m_curX * m_oneDivTileX, (tileY - 1 - m_curY) * m_oneDivTileY);
	}
}
