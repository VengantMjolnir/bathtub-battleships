using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

public static class Util_Array 
{
	public static void Shuffle<T>(this IList<T> list)
	{
		Random rng = new Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
