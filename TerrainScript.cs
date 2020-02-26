using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public int ancho = 512;
    public int alto = 512;

    public float zoom = 30.0f;
    public float desplazamientoX = 0.0f;
    public float desplazamientoY = 0.0f;

    public int altura = 15;
    Terrain suelo;

    public int speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        suelo = GetComponent<Terrain>();    
    }

    // Update is called once per frame
    void Update()
    {
        suelo.terrainData = crearSuelo(suelo.terrainData);
        desplazamientoX += speed * Time.deltaTime;
    }

    TerrainData crearSuelo(TerrainData terreno)
    {
        terreno.heightmapResolution = ancho + 1;
        terreno.size = new Vector3(ancho, altura, alto);
        terreno.SetHeights(0, 0, definirAlturas());
        return terreno;
    }

    float[,] definirAlturas()
    {
        float[,] alturas = new float[ancho, alto];

        for(int i = 0; i < ancho; i++)
        {
            for(int j = 0; j < alto; j++)
            {
                alturas[i, j] = nuevaAltura(i, j);
            }
        }
        return alturas;
    }

    float nuevaAltura(int i, int j)
    {
        float x = (float)i / ancho * zoom + desplazamientoX;
        float y = (float)j / alto * zoom + desplazamientoY;

        float ruido = Mathf.PerlinNoise(x, y);

        return ruido;
    }
}
