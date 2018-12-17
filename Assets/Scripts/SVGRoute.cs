using UnityEngine;
using Unity.VectorGraphics;
using System.IO;

public class SVGRoute : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ApplySVGPath();
    }

    void ApplySVGPath()
    {
        string svgPath = "Assets/RouteData/drawsvg.svg";

        SVGParser.SceneInfo sceneInfo = SVGParser.ImportSVG(new StreamReader(svgPath));
        Shape path = sceneInfo.NodeIDs["e1_polyline"].Shapes[0];        
        Debug.Log(path);

        BezierContour[] cs = path.Contours;
        BezierContour c = cs[0];
        //Debug.Log(c);

        BezierPathSegment[] ss = c.Segments;        
        Debug.Log($"SVGRoute segments count: {ss.Length}");
        for (int i = 0; i < ss.Length; i++) {
            BezierPathSegment s = ss[i];
            Debug.Log($"SVGRoute Segment points: {s.P0} -> {s.P1} -> {s.P2}");

            var debug1 = GameObject.Find($"SVGTarget{(i * 3) + 1}");
            var debug2 = GameObject.Find($"SVGTarget{(i * 3) + 2}");
            var debug3 = GameObject.Find($"SVGTarget{(i * 3) + 3}");
            debug1.transform.localPosition = s.P0;
            debug2.transform.localPosition = s.P1;
            debug3.transform.localPosition = s.P2;
            Debug.Log(debug3);
        }


        // debug1.transform.position = new Vector3(s.P0.x, 0.1f, s.P0.y);
        //     //(s.P0.x / 10) - 10f, 0.1f, (s.P0.y / 10) + 4.3f);
        // debug2.transform.position = new Vector3(s.P1.x, 0.1f, s.P1.y);
        // debug3.transform.position = new Vector3(s.P2.x, 0.1f, s.P2.y);
        var debug0 = GameObject.Find("SVGTarget0");
        debug0.transform.localPosition = Vector3.zero;

        //path.
        //var fill = shape.Fill as SolidFill;
        //fill.Color = Color.red;

        // ...
        //var geoms = VectorUtils.TessellateScene(sceneInfo.Scene, tessOptions);
        //var sprite = VectorUtils.BuildSprite(geoms, 100.0f, VectorUtils.Alignment.Center, Vector2.zero, 128, true); 
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
