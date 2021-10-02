using UnityEngine;

/**
 * Clase que recorre el arbol y ejecuta la acción pertinente teniendo en cuenta la propiedad booleana isBusy de la unidad
 */
public class DecisionTreeUnit : MonoBehaviour
{
	public DecisionTreeNode rootNode;
	public Unit unit;

	// Update is called once per frame
	void Update()
	{
		if (rootNode == null)
		{
			return;
		}

		IAction action = rootNode.Decide();

		if (action != null)
		{
            if (!unit.isBusy )
            {
				action.Act();
			}
			else if(unit.isBusy && action.ignoresBusy)
            {
				action.Act();
            }
		}
	}
}
