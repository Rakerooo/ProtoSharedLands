using Proto2.Input;
using UnityEngine;

namespace Proto2.Unit
{
    public class NewProtoUnitInputHandler : MonoBehaviour, INewProtoHoverable, INewProtoInteractable
    {
        [SerializeField] private NewProtoHero hero;
        
        public void OnHoverEnable()
        {
            hero.OnHoverEnable();
        }

        public void OnHoverDisable()
        {
            hero.OnHoverDisable();
        }

        public void OnMainClick()
        {
            hero.OnMainClick();
        }
    }
}
