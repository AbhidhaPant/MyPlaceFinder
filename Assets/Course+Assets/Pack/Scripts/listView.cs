using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

public class listView : MonoBehaviour
{
    private string prefabName = "listPrefab";
    // Start is called before the first frame update
    void Start()
    {
        if (SceneController.keyword != null)
        {
            StartCoroutine(makeURLReguest());
        }
    }
    // Update is called once per frame
    void Update()
    {   }
    IEnumerator makeURLReguest()
    {
        string url = "https://buildandrun.tv/GPSCourse/parks.Json.json";
        using (WWW www = new WWW(url))
        {
            yield return www;
            createList(www.text);
        }
    }

    public void createList(string jsonString)
    {
        if (jsonString != null)
        {
            RootObject thePlaces = new RootObject();
            Newtonsoft.Json.JsonConvert.PopulateObject(jsonString, thePlaces);
            if (thePlaces.results.Count != null)
            {
                string theWord = SceneController.keyword;

                switch (theWord)
                {
                    case "point_of_interest":
                        prefabName = "listPrefab";
                        break;
                    case "food":
                        prefabName = "listPrefabFood";
                        break;
                    case "hotel":
                        prefabName = "listPrefabHotel";
                        break;
                    case "park":
                        prefabName = "listPrefabPark";
                        break;
                    defalut:
                        break;
                }

                for (int i = 0; i < thePlaces.results.Count; i++)
                {
                    GameObject thePrefab = Instantiate(Resources.Load("ButtonPrefabs/" + prefabName)) as GameObject;
                    GameObject contentHolder = GameObject.FindGameObjectWithTag("Content");

                    thePrefab.transform.parent = contentHolder.transform;
                    Text[] theText = thePrefab.GetComponentsInChildren<Text>();
                    theText[0].text = thePlaces.results[i].name;

                    Debug.Log(theText[0].text+": "+ Math.Round(GeoCodeCalc.CalcDistance(23.203595025097528, 77.43602453744055, thePlaces.results[i].geometry.location.lat, thePlaces.results[i].geometry.location.lng, GeoCodeCalcMeasurement.Metre), 2));
                }
            }
            Debug.Log(thePlaces.results[0].name);
            Debug.Log(thePlaces.results[0].vicinity);
            Debug.Log(thePlaces.results[0].geometry.location.lat);
            Debug.Log(thePlaces.results[0].geometry.location.lng);

        }
    }
}

//JsonItem myDeserializedClass = JsonConvert.DeserializeObject<JsonItem>(myJsonResponse); 
//JsonItem jsonText = JsonConvert.DeserializeObject<JsonItem>(str);
public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Viewport
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
    public Viewport viewport { get; set; }
}

public class OpeningHours
{
    public bool open_now { get; set; }
}

public class Photo
{
    public int height { get; set; }
    public List<string> html_attributions { get; set; }
    public string photo_reference { get; set; }
    public int width { get; set; }
}

public class PlusCode
{
    public string compound_code { get; set; }
    public string global_code { get; set; }
}

public class Result
{
    public Geometry geometry { get; set; }
    public string icon { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public OpeningHours opening_hours { get; set; }
    public List<Photo> photos { get; set; }
    public string place_id { get; set; }
    public PlusCode plus_code { get; set; }
    public double rating { get; set; }
    public string reference { get; set; }
    public string scope { get; set; }
    public List<string> types { get; set; }
    public string vicinity { get; set; }
}

public class RootObject
{
    public List<object> html_attributions { get; set; }
    public List<Result> results { get; set; }
    public string status { get; set; }
}
