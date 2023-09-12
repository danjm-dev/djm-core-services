using UnityEngine;

namespace DJM.CoreUtilities.Services.MeshGeneration
{
    internal static class Plane
    {
        public static Mesh Create(int mapWidth, int mapHeight)
        {
            var vertices = new Vector3[(mapWidth + 1) * (mapHeight + 1)];
            var uv = new Vector2[vertices.Length];
            var normals = new Vector3[vertices.Length];
            
            for (var y = 0; y <= mapHeight; y++)
            {
                for (var x = 0; x <= mapWidth; x++)
                {
                    var index = y * (mapWidth + 1) + x;
                    vertices[index] = new Vector3(x, 0, y);
                    uv[index] = new Vector2((float)x / mapWidth, (float)y / mapHeight);
                    normals[index] = Vector3.up;
                }
            }
        
            var triangles = new int[mapWidth * mapHeight * 6];

            for (var y = 0; y < mapHeight; y++)
            {
                for (var x = 0; x < mapWidth; x++)
                {
                    var index = (y * mapWidth + x) * 6;
                    var vertexIndex = y * (mapWidth + 1) + x;
                    triangles[index] = vertexIndex;
                    triangles[index + 1] = vertexIndex + mapWidth + 1;
                    triangles[index + 2] = vertexIndex + 1;
                    triangles[index + 3] = vertexIndex + 1;
                    triangles[index + 4] = vertexIndex + mapWidth + 1;
                    triangles[index + 5] = vertexIndex + mapWidth + 2;
                }
            }

            return new Mesh
            {
                vertices = vertices,
                uv = uv,
                normals = normals,
                triangles = triangles
            };
        }     

    }
}