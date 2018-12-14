using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
//using UnityEditor.SceneManagement; //for setup, apparently in editor mode for play mode test - except that didn't work
using UnityEngine.AI;

namespace Tests
{
    public class RoadLaneTest //: IPrebuildSetup //: IMonoBehaviourTest
    {
        //public GameObject moveToActor;

        public void Setup()
        {
            //oh this is in Editor mode :o .. for play mode tests
            //SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
            //OpenScene("SampleScene");
            //UnityEditor.SceneManagement.LoadScene("SampleScene");
            //LoadScene(); //-- would not work: play mode call tells to use editor mode, which is not in assemblies
            //NOTE: is possible to add editor assembly (assemblies?) to tests .. i think, reading the docs earlier
        }

        public void LoadScene()
        {
            SceneManager.LoadScene("RoadNetworkScene", LoadSceneMode.Additive); //play mode unity engine call, called from a test
        }

        // A Test behaves as an ordinary method
        [Test]
        public void RoadLaneTestSimplePasses()
        {
            LoadScene();
            // Use the Assert class to test conditions

            //var actorGameObject = GameObject.Find("Cylinder");
            //var setupButton = actorGameObject.GetComponent<MoveTo>();

            //Start journey
            //Assert.AreEqual(1, 0);
            //moveToActor.GetComponent<MoveTo>(); //Update();
            //Debug.Log(actorGameObject);
            //Debug.Log(moveToActor.active);
            //Debug.Log(actorGameObject.activeSelf);
            Assert.AreEqual(1, 1);
            //cannot do anything here i guess cause all depends on the scene, even basic checks on road parts etc
        }

        GameObject GetActor()
        {
            return GameObject.Find("Biker");
        }

        GameObject GetTarget()
        {
            return GameObject.Find("BikeTarget");
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
                NavMeshAgent agent = a.GetComponent<NavMeshAgent>();
                NavMeshPath path = agent.path;
                Debug.Log(path);
                Vector3[] corners = path.corners;
                Debug.Log(corners);

                float dist = Vector3.Distance(a.transform.position, t.transform.position);
                Debug.Log(dist);
                if (dist < 0.1f) { //with close position of the origins in y dir goes to 0.007901428 now - so this has plenty margin. 10cm is considered 'there' in this traffic nav
                    break;
                }
                yield return false;
            }
            //yield return null;
        }

        //next / complex test? .. lanes and intersection?
        //or is something else more meaningful?
        //setting lane widths?

        //one test for car: do not cross bike path! (in wrong place / vain, at least?) .. maybe useful to see lanes go sensible, or actually should just check the surface but.. eh?

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
