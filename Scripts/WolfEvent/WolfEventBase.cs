using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wolf
{
    /// <summary>
    /// イベントの根底クラス。
    /// 必要な実装は、virtual IEnumerator ProcessEventと、 public static string searchTreePathの２つを
    /// かならず記述してください。
    /// 現在ノード作成時のフィールドを自動でできないか開発中。
    /// </summary>
    [System.Serializable]
    public class WolfEventBase : ScriptableObject
    {

        public int targetEvent = -1;

        [Header("node")]
        public float nodeX = 0;
        public float nodeY = 0;
        public static string searchTreePath = "Base";

        /// <summary>
        /// オーバーライドすることで、イベントの挙動を記述できます。
        /// オーバーライド先では、かならず最後にyield return base.ProcessEvent(source);を書いてください。
        /// 書くことで、次のイベントを実行できます。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual IEnumerator ProcessEvent(WolfEventSO source)
        {
            Debug.Log("hi this is event. next is " + targetEvent);
            // throw new System.NotImplementedException();
            if (targetEvent == -1)
                source.nextEvent = null;
            else
                source.nextEvent = source.wolfEvents[targetEvent];
            yield return null;
        }

        /// <summary>
        /// ノード作成時の挙動を描きます。 　Reflectionでクラスのフィールドの型とかを取得して、自動でノードのプロパティ部品を作れないか？
        /// </summary>
        /// <returns></returns>
        public virtual object GetNodeCreationInfo()
        {
            return "Hi!";
        }

    }
}