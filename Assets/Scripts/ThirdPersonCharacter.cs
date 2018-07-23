using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEditorInternal;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		private bool m_SwitchingLanes = false;
		private Vector3 m_EndPosition = new Vector3();

		Rigidbody m_Rigidbody;
		bool m_IsGrounded;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		CapsuleCollider m_Capsule;
		bool m_Crouching;
		


		void Start()
		{
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}


		public void Move(bool left, bool right)
		{
			float x = transform.position.x;
			float speed = 5;
			var forwardStep = Time.deltaTime * 50;
			if (m_SwitchingLanes)
			{
				if (Math.Abs(transform.position.x - m_EndPosition.x) > 0.01)
				{
					transform.position = Vector3.MoveTowards(transform.position, m_EndPosition, forwardStep);
				}
				else
				{
					m_SwitchingLanes = false;
				}
				
			}
			
			
			
			else if (left && !right)
			{
				if (Math.Abs(transform.position.x + 2.0f) < 0.01)
				{
					m_EndPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
					transform.position = Vector3.MoveTowards(transform.position, m_EndPosition, forwardStep);
				}
				else if (!m_SwitchingLanes)
				{
					m_SwitchingLanes = true;
					
					m_EndPosition = new Vector3(transform.position.x - 2.0f, transform.position.y, transform.position.z + speed);
					transform.position = Vector3.MoveTowards(transform.position, m_EndPosition, forwardStep);
				}
			} else if (right && !left)
			{
				if (Math.Abs(transform.position.x - 2.0f) < 0.01)
				{
					return;
				}
				if (!m_SwitchingLanes)
				{
					m_SwitchingLanes = true;
					m_EndPosition = new Vector3(transform.position.x + 2.0f, transform.position.y, transform.position.z + speed);
					transform.position = Vector3.MoveTowards(transform.position, m_EndPosition, forwardStep);
				}
			}
			else
			{
				m_EndPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
				transform.position = Vector3.MoveTowards(transform.position, m_EndPosition, forwardStep);
			}
			
		}

	}
}
