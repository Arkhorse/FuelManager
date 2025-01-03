namespace FuelManager
{
    internal static class Buttons
    {
        internal static Panel_Inventory_Examine _Panel_Inventory_Examine = new();
        internal static Vector3 GetBottomPosition(params Component[] components)
        {
            Vector3 result = new(0, 1000, 0);

            foreach (Component eachComponent in components)
            {
                if (eachComponent.gameObject.activeSelf && result.y > eachComponent.transform.localPosition.y)
                {
                    result = eachComponent.transform.localPosition;
                }
            }

            return result;
        }

        internal static bool IsSelected(UIButton button)
        {
            Panel_Inventory_Examine_MenuItem? menuItem = button.GetComponent<Panel_Inventory_Examine_MenuItem>();
            if (menuItem == null) return false;
            return menuItem.m_Selected;
        }

        internal static void SetButtonLocalizationKey(UIButton button, string key) => SetButtonLocalizationKey(button?.gameObject, key);

        internal static void SetButtonLocalizationKey(GameObject? gameObject, string key)
        {
            if (gameObject is not null)
            {
                bool wasActive = gameObject.activeSelf;
                gameObject.SetActive(false);

                UILocalize localize = gameObject.GetComponentInChildren<UILocalize>();
                if (localize != null)
                {
                    localize.key = key;
                }

                gameObject.SetActive(wasActive);
            }
        }

        internal static void SetButtonSprite(UIButton button, string sprite)
        {
            if (button is not null)
            {
                button.normalSprite = sprite;
            }
        }

        internal static void SetTexture(Component component, Texture2D texture)
        {
            if (component is null || texture is null)
            {
                return;
            }

            UITexture? uiTexture = component.GetComponent<UITexture>();
            if (uiTexture is null)
            {
                return;
            }

            uiTexture.mainTexture = texture;
        }

        internal static GameObject? GetChild(this GameObject gameObject, string childName)
        {
            return string.IsNullOrEmpty(childName)
                || gameObject == null
                || gameObject.transform == null
                || gameObject.transform.FindChild(childName) == null
                ? null
                : gameObject.transform.FindChild(childName).gameObject;
        }

        internal static void SetUnloadButtonLabel(Panel_Inventory_Examine panel, string localizationKey)
        {
            if (panel == null) return;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
            GameObject unloadPanel = GetChild(panel?.m_ExamineWidget?.gameObject, "UnloadRiflePanel");
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            SetButtonLocalizationKey(gameObject: unloadPanel, localizationKey);
        }
    }
}
