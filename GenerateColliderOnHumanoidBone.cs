using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class GenerateColliderOnHumanoidBone :MonoBehaviour {


	public Cloth cloth;

	[System.Serializable]
	public class ColliderUnit{
		public ColliderUnit(GameObject GO,CapsuleCollider collider,float radius){
			ColliderGO =GO;
			Collider = collider;
			Radius = radius;
		}
		public GameObject ColliderGO;
		[HideInInspector]
		public CapsuleCollider Collider;
		public float Radius;
	}
	public List<ColliderUnit> GenerateGOs = new List<ColliderUnit>();

	[ContextMenu("Generate")]
	public void Generate(){
		while(GenerateGOs.Count > 0){
			#if UNITY_EDITOR
				DestroyImmediate(GenerateGOs[0].ColliderGO);
			#else
				Destoy(GenerateGOs[0].ColliderGO);
			#endif
			GenerateGOs.RemoveAt(0);
		}
		Do(gameObject);
	}
	void Do(GameObject human){
		Animator animator = human.GetComponentInChildren<Animator>();
		if(animator.GetBoneTransform(HumanBodyBones.Hips) && animator.GetBoneTransform(HumanBodyBones.Spine))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.Hips),animator.GetBoneTransform(HumanBodyBones.Spine),0.1f,"Hips");
		}
		if(animator.GetBoneTransform(HumanBodyBones.Spine) && animator.GetBoneTransform(HumanBodyBones.Head))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.Spine),animator.GetBoneTransform(HumanBodyBones.Head),0.1f,"Spine");
		}

		if(animator.GetBoneTransform(HumanBodyBones.LeftUpperArm) && animator.GetBoneTransform(HumanBodyBones.LeftLowerArm))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.LeftUpperArm),animator.GetBoneTransform(HumanBodyBones.LeftLowerArm),0.1f,"LeftUpperArm");
		}
		if(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm) && animator.GetBoneTransform(HumanBodyBones.LeftHand))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm),animator.GetBoneTransform(HumanBodyBones.LeftHand),0.1f,"LeftLowerArm");
		}
		if(animator.GetBoneTransform(HumanBodyBones.RightUpperArm) && animator.GetBoneTransform(HumanBodyBones.RightLowerArm))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.RightUpperArm),animator.GetBoneTransform(HumanBodyBones.RightLowerArm),0.1f,"RightUpperArm");
		}
		if(animator.GetBoneTransform(HumanBodyBones.RightLowerArm) && animator.GetBoneTransform(HumanBodyBones.RightHand))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.RightLowerArm),animator.GetBoneTransform(HumanBodyBones.RightHand),0.1f,"RightLowerArm");
		}

		if(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg) && animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg),animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg),0.1f,"LeftUpperLeg");
		}
		if(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg) && animator.GetBoneTransform(HumanBodyBones.LeftFoot))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg),animator.GetBoneTransform(HumanBodyBones.LeftFoot),0.1f,"LeftLowerLeg");
		}
		if(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg) && animator.GetBoneTransform(HumanBodyBones.RightLowerLeg))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.RightUpperLeg),animator.GetBoneTransform(HumanBodyBones.RightLowerLeg),0.1f,"RightUpperLeg");
		}
		if(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg) && animator.GetBoneTransform(HumanBodyBones.RightFoot))
		{
			GenerateCapsuleCollider(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg),animator.GetBoneTransform(HumanBodyBones.RightFoot),0.1f,"RightLowerLeg");
		}
	}

	void GenerateCapsuleCollider(Transform from,Transform to,float radius,string name){

		GameObject colliderGO = new GameObject(name);

		colliderGO.transform.parent = from;
		colliderGO.transform.localPosition = Vector3.zero;
		colliderGO.transform.rotation = Quaternion.LookRotation(to.position - from.position);

		CapsuleCollider cc = colliderGO.gameObject.AddComponent<CapsuleCollider>();
		cc.direction = 2;


		float dis = Vector3.Distance(from.position,to.position);
		cc.height = dis;
		cc.center = new Vector3(0,0,dis / 2);
		cc.radius = radius;

		GenerateGOs.Add(new ColliderUnit( colliderGO ,cc, radius));
	}




	void Update(){
		#if UNITY_EDITOR
		foreach(ColliderUnit cu in GenerateGOs){
			cu.Collider.radius = cu.Radius;
		}
		if(cloth != null)
		{
			List<CapsuleCollider> cclist = new List<CapsuleCollider>();
			foreach(ColliderUnit cu in GenerateGOs){
				cclist.Add(cu.Collider);
			}
			cloth.capsuleColliders = cclist.ToArray();
		}
		#endif

	}

}
