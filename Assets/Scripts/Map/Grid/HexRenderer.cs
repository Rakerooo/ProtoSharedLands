using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map.Grid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class HexRenderer : MonoBehaviour
    {
        private Mesh mesh;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private MeshCollider meshCollider;
        
        private const string meshName = "Hex";

        [SerializeField] private Material material;
        [SerializeField] private float innerSize;
        [SerializeField] private float outerSize;
        [SerializeField] private float height;
        [SerializeField] private bool isFlatTopped;

        private List<Face> faces;

        public void InstantiateRenderer(Material _material, float _innerSize, float _outerSize, float _height, bool _isFlatTopped)
        {
            material = _material;
            innerSize = _innerSize;
            outerSize = _outerSize;
            height = _height;
            isFlatTopped = _isFlatTopped;
            CreateMesh();
            
            DrawMesh();
            CombineFaces();
        }
        
        [ContextMenu("Refresh")]
        private void Refresh()
        {
            CreateMesh();
            
            meshFilter.mesh = mesh;
            meshRenderer.material = material;
            
            DrawMesh();
            CombineFaces();
        }

        private void CreateMesh()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
            
            mesh = new Mesh
            {
                name = meshName
            };
            
            meshFilter.mesh = mesh;
            meshRenderer.material = material;
            meshCollider.sharedMesh = mesh;
        }
        
        private void DrawMesh()
        {
            faces = new List<Face>();
            
            for (var point = 0; point < 6; point++)
            {
                // Top faces
                faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
                
                // Bottom faces
                faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point));
                
                // Outer faces
                faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
                
                if (innerSize <= 0)
                {
                    // Inner faces
                    faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point));
                }
            }
        }
        
        private void CombineFaces()
        {
            if (mesh == null) return;
            
            var vertices = new List<Vector3>();
            var tris = new List<int>();
            var uvs = new List<Vector2>();

            for (var i = 0; i < faces.Count; i++)
            {
                var face = faces[i];
                
                // Add the vertices
                vertices.AddRange(face.vertices);
                uvs.AddRange(face.uvs);

                // Offset the triangles
                var offset = 4 * i;
                tris.AddRange(face.triangles.Select(triangle => triangle + offset));
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = tris.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.RecalculateNormals();
        }

        private Face CreateFace(float _innerRad, float _outerRad, float _heightA, float _heightB, int _point, bool _reverse = false)
        {
            var pointA = GetPoint(_innerRad, _heightB, _point);
            var pointB = GetPoint(_innerRad, _heightB, _point < 5 ? _point + 1 : 0);
            var pointC = GetPoint(_outerRad, _heightA, _point < 5 ? _point + 1 : 0);
            var pointD = GetPoint(_outerRad, _heightA, _point);

            var vertices = new List<Vector3> { pointA, pointB, pointC, pointD };
            var triangles = new List<int> { 0, 1, 2, 2, 3, 0 };
            var uvs = new List<Vector2> { new(0, 0), new(1, 0), new(1, 1), new(0, 1) };
            if (_reverse) vertices.Reverse();
            
            return new Face(vertices, triangles, uvs);
        }
        
        private Vector3 GetPoint(float _size, float _height, int _index)
        {
            var degAngle = isFlatTopped ? 60f * _index : 60f * _index - 30;
            var radAngle = Mathf.PI / 180f * degAngle;
            
            return new Vector3(_size * Mathf.Cos(radAngle), _height, _size * Mathf.Sin(radAngle));
        }
    }
}
