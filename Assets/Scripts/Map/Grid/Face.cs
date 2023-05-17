using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public struct Face
    {
        public List<Vector3> vertices { get; private set; }
        public List<int> triangles { get; private set; }
        public List<Vector2> uvs { get; private set; }

        public Face(List<Vector3> _vertices, List<int> _triangles, List<Vector2> _uvs)
        {
            vertices = _vertices;
            triangles = _triangles;
            uvs = _uvs;
        }
    
    }
}
