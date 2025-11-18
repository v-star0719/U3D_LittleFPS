using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightLine : MonoBehaviour
{
	//public float lineWidth = 0.2f;
	//public float lineLength = 10f;

	//public GameObject cube;
	//public ActorBodyPoints bodyPoints;
	//public MeshFilter meshFilter;

	//private Mesh mesh = null;

	// Use this for initialization
	void Start ()
	{
		//	if (mesh == null)
		//		mesh = new Mesh();
		//	meshFilter.mesh = mesh;
		//
	}
	
	// Update is called once per frame
	void Update ()
	{

		//cube.transform.LookAt();

		//Vector3[] points = new Vector3[4];
		//points[0] = bodyPoints.bulletOriginPos.localPosition + new Vector3(lineWidth * 0.5f, 0, 0);
		//points[1] = bodyPoints.bulletOriginPos.localPosition - new Vector3(lineWidth * 0.5f, 0, 0);
		//points[2] = bodyPoints.bulletOriginPos.localPosition + 
		//	(bodyPoints.bulletDirPos.localPosition - bodyPoints.bulletOriginPos.localPosition) * lineLength + 
		//	new Vector3(lineWidth * 0.5f, 0, 0);
		//points[2] = bodyPoints.bulletOriginPos.localPosition +
		//            (bodyPoints.bulletDirPos.localPosition - bodyPoints.bulletOriginPos.localPosition) * lineLength -
		//            new Vector3(lineWidth * 0.5f, 0, 0);
		//for(int i=0; i<4; i++)
		//	points[i] = bodyPoints.bulletOriginPos.localToWorldMatrix.MultiplyPoint(points[i]);

		//mesh.SetVertices(new List<Vector3>(points));

		//int[] indices = new int[3]{0, 1, 2};
		//mesh.SetIndices(indices, MeshTopology.Triangles, 0);
	}
}
