using System;
using System.Collections.Generic;
using UnityEngine;

class SMnodeLoader
{
    public Part part;
    public string moduleName;
    public string nodeName;
    public string valueName;
    public List<string> valueList = new List<string>();
    float[] trimArray = new float[1];
    public bool testOnloadMemory = false;
    public bool foundExistingNodes = false;
    string[] values = new string[1];
    //string[] 
    //List<string> moduleIDList = new List<string>(); // seems lists aren't copied from OnLoad to a new part, do filling a list with just the ID will test for whether this is a new part
    [KSPField]
    public string moduleID = "0"; // only needed in the orginal onLoad from the part.cfg to differentiate similar modules. After that, they will opnly check their internal nodes.
    public bool debugMode = false;

    public SMnodeLoader(Part _part, string _moduleName, string _moduleID, string _nodeName, string _valueName)
    {
        part = _part;
        moduleName = _moduleName;
        moduleID = _moduleID;
        nodeName = _nodeName;
        valueName = _valueName;
    }
    public FloatCurve ProcessNodeAsFloatCurve(ConfigNode node)
    {
        FloatCurve resultCurve = new FloatCurve();
        ConfigNode[] moduleNodeArray = node.GetNodes(nodeName);
        for (int k = 0; k < moduleNodeArray.Length; k++)
        {
            string[] valueArray = moduleNodeArray[k].GetValues(valueName);
            for (int l = 0; l < valueArray.Length; l++)
            {
                string[] splitString = valueArray[l].Split(' ');
                try
                {
                    Vector2 v2 = new Vector2(float.Parse(splitString[0]), float.Parse(splitString[1]));
                    resultCurve.Add(v2.x, v2.y, 0, 0);
                }
                catch
                {
                    Debug.Log("Error parsing vector2");
                }
            }
        }
        return resultCurve;
    }

    public List<String> ProcessNodeAsStringList(ConfigNode node)
    {
        List<String> resultList = new List<string>();
        ConfigNode[] moduleNodeArray = node.GetNodes(nodeName);
        for (int k = 0; k < moduleNodeArray.Length; k++)
        {
            string[] valueArray = moduleNodeArray[k].GetValues(valueName);
            for (int l = 0; l < valueArray.Length; l++)
            {
                resultList.Add(valueArray[l]);
            }
        }
        return resultList;
    }

    [Obsolete("Use processNode and fill a static list from OnLoad instead", true)]
    public List<String> OnStart()
    {        
        ConfigNode[] nodes;        
        
        if (valueList.Count == 0)
        {
            if (part.partInfo != null)
            {
                // fill trimList from part.cfg module
                UrlDir.UrlConfig[] cfg = GameDatabase.Instance.GetConfigs("PART");
                for (int i = 0; i < cfg.Length; i++)
                {
                    if (part.partInfo.name == cfg[i].name)
                    {
                        nodes = cfg[i].config.GetNodes("MODULE");
                        for (int j = 0; j < nodes.Length; j++)
                        {
                            if (nodes[j].GetValue("name") == moduleName)
                            {

                                bool correctModuleFound = false;
                                string[] IDArray = nodes[j].GetValues("moduleID");
                                if (IDArray.Length > 0)
                                {
                                    if (IDArray[0] == moduleID)
                                        correctModuleFound = true;
                                    else
                                        correctModuleFound = false;
                                }
                                else
                                {
                                    moduleID = "0";
                                    correctModuleFound = true;
                                }

                                if (correctModuleFound)
                                {
                                    valueList = ProcessNodeAsStringList(nodes[j]);
                                    //ConfigNode[] moduleNodeArray = nodes[j].GetNodes(nodeName);
                                    //debugMessage("moduleNodeArray.length " + moduleNodeArray.Length);
                                    //for (int k = 0; k < moduleNodeArray.Length; k++)
                                    //{
                                    //    debugMessage("found node");
                                    //    string[] valueArray = moduleNodeArray[k].GetValues(valueName);
                                    //    debugMessage("found " + valueArray.Length + " values");
                                    //    for (int l = 0; l < valueArray.Length; l++)
                                    //    {
                                    //        debugMessage("Adding value to node " + valueArray[l]);
                                    //        valueList.Add(valueArray[l]);
                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }
        return valueList;
    }

    public void OnLoad(ConfigNode node)
    {
        testOnloadMemory = true;

        // ("OnLoad testOnLoadMemory set to " + testOnloadMemory);

        ConfigNode[] existingNodes = node.GetNodes(nodeName);
        if (existingNodes.Length > 0)
        {
            for (int i = 0; i < existingNodes.Length; i++)
            {


                values = existingNodes[i].GetValues(valueName);
                for (int j = 0; j < values.Length; j++)
                {
                    valueList.Add(values[j]);
                }
                //if (values.Length > 0)
                if (valueList.Count > 0)
                {
                    foundExistingNodes = true;
                    //trimArray = new float[trimList.Count];
                    //for (int ta = 0; ta < trimList.Count; ta++)
                    //{
                    //    trimArray[ta] = trimList[ta];
                    //}
                }
                else
                {
                    foundExistingNodes = false;
                }
            }
        }
        else
        {
            foundExistingNodes = false;
        }
    }

    public ConfigNode OnSave(ConfigNode node)
    {        
        ConfigNode newNode = new ConfigNode(nodeName);
        for (int i = 0; i < valueList.Count; i++)
        {
            newNode.AddValue(valueName, valueList[i]);
        }
        node.AddNode(newNode);
        return node;
    }
}
