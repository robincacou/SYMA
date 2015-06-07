using UnityEngine;
using System.Collections;

public class NameGenerator : MonoBehaviour {

	// No Q or Y
	static char[] vowels = new char[]{'a', 'e', 'i', 'o', 'u'};
	static char[] consonants = new char[]{'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 's', 't', 'v', 'w', 'x', 'z'};

	public static string GenerateName()
	{
		int nameSize = Random.Range(4, 6);
		bool startWithVowel = Random.Range(0, 2) == 1;
		string res = "";
		for (int i = 0; i < nameSize; i++)
		{
			if (startWithVowel)
				res += vowels[Random.Range(0, vowels.Length)];
			else
				res += consonants[Random.Range(0, consonants.Length)];

			startWithVowel = !startWithVowel;
		}
		res = res.Substring(0,1).ToUpper() + res.Substring(1);
		return res;
	}

	public static string GenerateStationName()
	{
		string res = GenerateName();

		switch(Random.Range(0, 8))
		{
		case 0:
			res += " en ";
			break;

		case 1:
			res += " les ";
			break;

		case 2:
			res += " le ";
			break;

		case 3:
			res += " sur ";
			break;

		case 4:
			res += " la ";
			break;

		case 5:
			res = "La " + res + " ";
			break;

		case 6:
			res = "Le " + res + " ";
			break;

		case 7:
			res += "-";
			break;

		default:
			res += " ";
			break;
		}

		res += GenerateName();
		return res;
	}
}
