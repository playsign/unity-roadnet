using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
//using UnityEditor.SceneManagement; //for setup, apparently in editor mode for play mode test - except that didn't work
using UnityEngine.AI;
//using Unity.VectorGraphics; //svg handling now on app side, not in test

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
            //Bike & Target
            GameObject b = null;
            GameObject bt = null;
            //Car & Target
            GameObject c = null;
            GameObject ct = null;
            while (b == null || bt == null || c == null || ct == null) {
            //doesn't really make sense cause these all come at scene load. don't know if could make sense with some open data dataset or other test conf loading
            //anyhow this actually makes it somehow clear that is about load wait / test dependencies:
                b = GameObject.Find("Biker");
                bt = GameObject.Find("BikeTarget"); //there is also 2 behind the corner, not used yet (would work for this test too)

                c = GameObject.Find("Car");
                ct = GameObject.Find("CarTarget3"); //one for the complete test with lotsa turns with also bike lane on the side & then crossing the same road behind and the lane to other dir, with NavMesh Link

                yield return false;
            }
            Debug.Log($"{b.name} : {b} -> {bt}");
            Debug.Log($"{c.name} : {c} -> {ct}");

            //ApplySVGPath();

            while (true) {
                if (AgentAtDestination(b, bt)
                 && AgentAtDestination(c, ct)) {
                    break;
                }
                yield return false;
            }
            //yield return null;
        }

        bool AgentAtDestination(GameObject a, GameObject t)
        {
            NavMeshAgent agent = a.GetComponent<NavMeshAgent>();
            //NavMeshPath path = agent.path;
            //Debug.Log(path);
            //Vector3[] corners = path.corners;
            //Debug.Log(corners);

            float dist = Vector3.Distance(a.transform.position, t.transform.position);
            Debug.Log(dist);
            return (dist < 1f);
            //with close position of the origins in y dir goes to 0.007901428 now - so this has plenty margin. 10cm is considered 'there' in this traffic nav
            //now upped to 1 to use for coarse used-right-road-segment (some part of the lange, right way etc) route taking / wayfinding check
            //also: does not affect driving behaviour, just acknowledges the waypoint in test - the actor still continues to go close to it, if it's the target
        }

        //next / complex test? .. lanes and intersection?
        //DONE now with surface types & NavMesh Link in the test scene - test does not check yet that the lane biz works, but it shows visually

        //one test for car: do not cross bike path! (in wrong place / vain, at least?) .. maybe useful to see lanes go sensible, or actually should just check the surface but.. eh?

        //or is something else more meaningful?
        //setting lane widths?


/* moved to the application side, with the idea that is used to show roads etc.
also, triggered to make the move when encountered not having the extra vectorgraphics namespace in test assembly.
which is maybe good
    void ApplySVGPath()
    {
        string svgPath = "";
        var sceneInfo = SVGParser.ImportSVG(new StreamReader(svgPath));
        var path = sceneInfo.NodeIDs["e1_polyline"]; //.Shapes[0];
        Debug.Log(path);
        //var fill = shape.Fill as SolidFill;
        //fill.Color = Color.red;

        // ...
        //var geoms = VectorUtils.TessellateScene(sceneInfo.Scene, tessOptions);
        //var sprite = VectorUtils.BuildSprite(geoms, 100.0f, VectorUtils.Alignment.Center, Vector2.zero, 128, true); 
    }
*/


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
