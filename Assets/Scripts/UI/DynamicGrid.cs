using UnityEngine;
using UnityEngine.UI;

namespace NewOverlord
{
	public class DynamicGrid : MonoBehaviour
	{
		[SerializeField] GridLayoutGroup gridContainer = null;
		[SerializeField] Vector2 baseCellSize = new Vector2(280f, 70f);
		[SerializeField] Vector2 baseSpace = new Vector2(0f, 7f);
		[SerializeField] Vector2 baseResolution = new Vector2(1280f, 720f);
		
		float widthScale = 0f;
		float heightScale = 0f;
		Vector2 tempSize = Vector2.zero;
		Vector2 tempSpace = Vector2.zero;

		private void Awake()
		{
			gridContainer = gridContainer.GetComponent<GridLayoutGroup>();
			widthScale = Screen.currentResolution.width / baseResolution.x;
			heightScale = Screen.currentResolution.height / baseResolution.y;
		}

		void Update()
		{
			tempSize.Set(baseCellSize.x * widthScale, baseCellSize.y * heightScale);
			tempSpace.Set(baseSpace.x * widthScale, baseSpace.y * heightScale);
			gridContainer.cellSize = tempSize;
			gridContainer.spacing = tempSpace;
		}
	}
}