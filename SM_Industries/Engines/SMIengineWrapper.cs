using System.Linq;
using UnityEngine;

namespace SMIndustries.engine
{
    class SMIengineWrapper
    {
        public enum EngineType
        {
            NONE,
            ModuleEngine,
            ModuleEngineFX,
            SMIengine,
           // MultiModeEngine,
        }
        public EngineType type = EngineType.NONE;
        public ModuleEngines engine;
        public ModuleEnginesFX engineFX;
        public SMIengine smiengine;
       public MultiModeEngine engineFX2;

        public SMIengineWrapper(Part part)
        {
            engine = part.Modules.OfType<ModuleEngines>().FirstOrDefault();
            if (engine != null)
            {
                type = EngineType.ModuleEngine;
            }
            else
            {
                engineFX = part.Modules.OfType<ModuleEnginesFX>().FirstOrDefault();
                if (engineFX != null)
                {
                    type = EngineType.ModuleEngineFX;
                }
                else                                                      //multimode
                {
                  engineFX2 = part.Modules.OfType<MultiModeEngine>().FirstOrDefault(); //multimode
                 if (engineFX != null)
                   {
                    type = EngineType.ModuleEngineFX;                               //multimode
                    }                                          
                   else
                {
                    smiengine = part.Modules.OfType<SMIengine>().FirstOrDefault();
                    if (smiengine != null)
                    {
                        type = EngineType.SMIengine;
                    }
                }
            }
            Debug.Log("SMIengineWrapper: engine type is " + type.ToString());
        }
    }


    public SMIengineWrapper(Part part, string name)
    {
        engineFX = part.Modules.OfType<ModuleEnginesFX>().Where(p => p.engineID == name).FirstOrDefault();
        if (engineFX != null)
            type = EngineType.ModuleEngineFX;
        //Debug.Log("SMIengineWrapper: engine type is " + type.ToString());

    }

    public float maxThrust
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.maxThrust;
                case EngineType.ModuleEngineFX:
                    return engineFX.maxThrust;
                case EngineType.SMIengine:
                    return smiengine.maxThrust;
                default:
                    return 0f;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.maxThrust = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.maxThrust = value;
                    break;
                case EngineType.SMIengine:
                    smiengine.maxThrust = value;
                    break;
            }
        }
    }

    public float minThrust
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.minThrust;
                case EngineType.ModuleEngineFX:
                    return engineFX.minThrust;
                //case EngineType.SMIengine:
                //    return smiengine.minThrust;
                default:
                    return 0f;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.minThrust = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.minThrust = value;
                    break;
                    //case EngineType.SMIengine:
                    //    smiengine.minThrust = value;
                    //    break;
            }
        }
    }

    public bool EngineIgnited
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.EngineIgnited;
                case EngineType.ModuleEngineFX:
                    return engineFX.EngineIgnited;
                case EngineType.SMIengine:
                    return smiengine.EngineIgnited;
                default:
                    return false;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.EngineIgnited = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.EngineIgnited = value;
                    break;
                case EngineType.SMIengine:
                    smiengine.EngineIgnited = value;
                    break;
            }
        }
    }

    public bool flameout
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.flameout;
                case EngineType.ModuleEngineFX:
                    return engineFX.flameout;
                case EngineType.SMIengine:
                    return smiengine.flameout;
                default:
                    return false;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.flameout = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.flameout = value;
                    break;
                case EngineType.SMIengine:
                    smiengine.flameout = value;
                    break;
            }
        }
    }

    public bool getIgnitionState
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.getIgnitionState;
                case EngineType.ModuleEngineFX:
                    return engineFX.getIgnitionState;
                case EngineType.SMIengine:
                    return smiengine.EngineIgnited;
                default:
                    return false;
            }
        }
    }

    public bool getFlameoutState
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.getFlameoutState;
                case EngineType.ModuleEngineFX:
                    return engineFX.getFlameoutState;
                case EngineType.SMIengine:
                    return smiengine.flameout;
                default:
                    return false;
            }
        }
    }

    public float engineAccelerationSpeed
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.engineAccelerationSpeed;
                case EngineType.ModuleEngineFX:
                    return engineFX.engineAccelerationSpeed;
                case EngineType.SMIengine:
                    return smiengine.powerProduction;  // not an accurate alternative
                default:
                    return 0f;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.engineAccelerationSpeed = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.engineAccelerationSpeed = value;
                    break;
                case EngineType.SMIengine:
                    smiengine.powerProduction = value;  // not an accurate alternative
                    break;
            }
        }
    }

    public float engineDecelerationSpeed
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.engineDecelerationSpeed;
                case EngineType.ModuleEngineFX:
                    return engineFX.engineDecelerationSpeed;
                case EngineType.SMIengine:
                    return smiengine.powerDrain; // not an accurate alternative
                default:
                    return 0f;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.engineDecelerationSpeed = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.engineDecelerationSpeed = value;
                    break;
                case EngineType.SMIengine:
                    smiengine.powerDrain = value;  // not an accurate alternative
                    break;
            }
        }
    }

    public float finalThrust
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.finalThrust;
                case EngineType.ModuleEngineFX:
                    return engineFX.finalThrust;
                case EngineType.SMIengine:
                    return smiengine.finalThrust; // not an accurate alternative
                default:
                    return 0f;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.finalThrust = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.finalThrust = value;
                    break;
                case EngineType.SMIengine:
                    smiengine.finalThrust = value;  // not an accurate alternative
                    break;
            }
        }
    }

    public float finalThrustNormalized
    {
        get
        {
            return finalThrust / maxThrust;
        }
    }

    public bool throttleLocked
    {
        get
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    return engine.throttleLocked;
                case EngineType.ModuleEngineFX:
                    return engineFX.throttleLocked;
                //case EngineType.SMIengine:
                //    return smiengine.throttleLocked;
                default:
                    return false;
            }
        }
        set
        {
            switch (type)
            {
                case EngineType.ModuleEngine:
                    engine.throttleLocked = value;
                    break;
                case EngineType.ModuleEngineFX:
                    engineFX.throttleLocked = value;
                    break;
                    //case EngineType.SMIengine:
                    //    smiengine.flameout = value;
                    //    break;
            }
        }
    }
}
    }


