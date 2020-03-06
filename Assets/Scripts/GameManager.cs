using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Tracer
{
	public class GameManager : MonoBehaviour
	{
		public Animator Panel;
		public TMP_Dropdown Dropdown;
		public Transform Camera;
		public List<GameObject> Prefabs;

		private static readonly int Open = Animator.StringToHash("Opened");

		private void Start()
		{
			string json = File.ReadAllText(@"C:\Users\Itorius\Desktop\test.json", Encoding.UTF8);

			var container = JsonUtility.FromJson<ListContainer>(json);

			foreach (SaveData data in container.list)
			{
				var prefab = Prefabs.First(gameObject => gameObject.name == data.element);
				var obj =	Instantiate(prefab, data.position, Quaternion.AngleAxis(data.rotation, Vector3.forward));
				obj.tag = "Saveable";
				obj.name = prefab.name;
				obj.transform.localScale = data.scale;
			}
			
			List<TMP_Dropdown.OptionData> options = Prefabs.Select(gameObject => new TMP_Dropdown.OptionData(gameObject.name)).ToList();
			Dropdown.AddOptions(options);
		}

		public void FoldMenu()
		{
			bool open = Panel.GetBool(Open);
			Panel.SetBool(Open, !open);
		}

		public void AddItem()
		{
			var cameraPos = Camera.position;
			var spawnPos = new Vector3(cameraPos.x, cameraPos.y, 0);

			var prefab = Prefabs.First(o => o.name == Dropdown.options[Dropdown.value].text);
			var obj = Instantiate(prefab, spawnPos, Quaternion.AngleAxis(-90f, Vector3.forward));
			obj.tag = "Saveable";
			obj.name = prefab.name;
		}

		public void Quit()
		{
			var elements = GameObject.FindGameObjectsWithTag("Saveable");

			List<SaveData> data = elements.Select(element => element.transform).Select(transform => new SaveData
			{
				element = transform.name,
				position = transform.position,
				rotation = transform.rotation.eulerAngles.z,
				scale = transform.localScale
			}).ToList();

			string s = JsonUtility.ToJson(new ListContainer(data), true);

			File.WriteAllText(@"C:\Users\Itorius\Desktop\test.json", s, Encoding.UTF8);

			Application.Quit();
		}
	}

	[Serializable]
	public struct SaveData
	{
		public string element;
		public Vector2 position;
		public float rotation;
		public Vector2 scale;
	}

	[Serializable]
	public struct ListContainer
	{
		public List<SaveData> list;

		public ListContainer(List<SaveData> list)
		{
			this.list = list;
		}
	}
}