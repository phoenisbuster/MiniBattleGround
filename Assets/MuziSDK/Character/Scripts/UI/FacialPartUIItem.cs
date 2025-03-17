using System;
using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MuziCharacter.UI
{

    public class FacialPartUIItem : MonoBehaviour
    {
        [SerializeField]
        Button mainButton;

        [SerializeField]
        TextMeshProUGUI numberText;



        [SerializeField]
        List<FacialSpritePair> predefinedSprites;


        [SerializeField]
        Image partImage;

        public event Action<FacePartName, int> OnChoose;

        public Action<Item> OnChildTemplateUIChoose;

        private FacePartName myPart;
        private int partId;

        private Item currentTemplate;
        public void SetData(Item template)
        {
            currentTemplate = template;
            SetText(template.title.ToString());
            // SetSprite(p);
        }

        public void SetData(FacePartName p, int id)
        {
            myPart = p;
            partId = id;

            SetText(id.ToString());
            SetSprite(p);

            // a random for presenting different shape

            if (p == FacePartName.Eyesbrown)
            {
                (mainButton.transform as RectTransform).localScale = new Vector3(2f, 1f, 1f);
            }
            else
            {
                var randW = UnityEngine.Random.Range(50f, 65f);
                (mainButton.transform as RectTransform).sizeDelta = new Vector2(randW, randW);
                (mainButton.transform as RectTransform).localRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-5f, 25f));
            }
        }


        void SetText(string text) => numberText.text = text;

        void SetSprite(FacePartName p) => partImage.sprite = predefinedSprites.First(e => e.Part == p).Sprite;

        private void Start()
        {
            mainButton.onClick.AddListener(() =>
            {
                OnChoose?.Invoke(myPart, partId);
                
                OnChildTemplateUIChoose?.Invoke(currentTemplate);
            });
        }
    }

    [Serializable]
    public class FacialSpritePair
    {
        public Sprite Sprite;
        public FacePartName Part;
    }


    public enum FacePartName
    {
        CheekShape,
        NoseShape,
        MouthShape,
        EyeShape,

        EyesMakeup,
        Eyesbrown,
        Lips
    }
}