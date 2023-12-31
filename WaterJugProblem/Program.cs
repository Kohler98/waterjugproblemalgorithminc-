using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WaterJugProblem.handler;
public class Program
{
    public static void Main()
    {
        int bucketX = 2; // Capacidad del cubo X
        int bucketY = 100; // Capacidad del cubo Y
        int target = 90; // Cantidad deseada de agua

        List<string> solution = HandlerWaterJugProblem.SolveWaterBucketProblem(bucketX, bucketY, target);

        foreach (string step in solution)
        {
            Console.WriteLine(step);
        }
    }

}
