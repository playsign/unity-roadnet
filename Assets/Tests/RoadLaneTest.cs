using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
//using UnityEditor.SceneManagement; //for setup, apparently in editor mode for play mode test - except that didn't work

namespace Tests
{
    public class RoadLaneTest : IPrebuildSetup //: IMonoBehaviourTest
    {
        public GameObject moveToActor;

        public void Setup()
        {
            //oh this is in Editor mode :o .. for play mode tests
            //SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
            //OpenScene("SampleScene");
            //UnityEditor.SceneManagement.LoadScene("SampleScene");
            //LoadScene(); //-- would not work: play mode call tells to use editor mode, which is not in assemblies
        }

        public void LoadScene()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void RoadLaneTestSimplePasses()
        {
            LoadScene();
            // Use the Assert class to test conditions

            var actorGameObject = GameObject.Find("Cylinder");
            //var setupButton = actorGameObject.GetComponent<MoveTo>();

            //Start journey
            //Assert.AreEqual(1, 0);
            //moveToActor.GetComponent<MoveTo>(); //Update();
            Debug.Log(actorGameObject);
            //Debug.Log(moveToActor.active);
            //Debug.Log(actorGameObject.activeSelf);
            Assert.AreEqual(1, 1);
        }

        GameObject GetActor()
        {
            return GameObject.Find("Cylinder");
        }

        GameObject GetTarget()
        {
            return GameObject.Find("Sphere");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator RoadLaneTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            
            //actor & target
            GameObject a = null;
            GameObject t = null;
            while (a == null || t == null) {
                a = GetActor();
                t = GetTarget();
                yield return false;
            }
            Debug.Log($"{a} -> {t}");

            while (true) {
                float dist = Vector3.Distance(a.transform.position, t.transform.position);
                Debug.Log(dist);
                if (dist < 0.1f) { //with close position of the origins in y dir goes to 0.007901428 now - so this has plenty margin. 10cm is considered 'there' in this traffic nav
                    break;
                }
                yield return false;
            }
            //yield return null;
        }

/*
        public bool IsTestFinished
        { 
            get 
            {
                return true;
            }
        }
*/
    }
}
