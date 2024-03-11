using UnityEngine;

public class UnitActionParameters
{
    private UnitActionParameters() {}
    public UnitActionScriptableObject ScriptableObject { get; private set; }
    public BattleUnitBase Owner { get; private set; }
    public BattleUnitBase Target { get; private set; }
    public EnhancedObject EnhancedObject { get; private set; }
    public Vector3? Position { get; private set; }
    public bool IsBuilded { get; private set; }


    public class UnitActionParametersBuilder
    {
        private readonly UnitActionParameters _unitActionParameters;

        public UnitActionParametersBuilder()
        {
            _unitActionParameters = new UnitActionParameters();
        }

        public UnitActionParametersBuilder SetScriptableObject(UnitActionScriptableObject scriptableObject)
        {
            _unitActionParameters.ScriptableObject = scriptableObject;
            return this;
        }

        public UnitActionParametersBuilder SetOwner(BattleUnitBase owner)
        {
            _unitActionParameters.Owner = owner;
            return this;
        }

        public UnitActionParametersBuilder SetTarget(BattleUnitBase target)
        {
            _unitActionParameters.Target = target;
            return this;
        }

        public UnitActionParametersBuilder SetEnhancedObject(EnhancedObject enhancedObject)
        {
            _unitActionParameters.EnhancedObject = enhancedObject;
            return this;
        }

        public UnitActionParametersBuilder SetPosition(Vector3 position)
        {
            _unitActionParameters.Position = position;
            return this;
        }

        public UnitActionParameters Build()
        {
            _unitActionParameters.IsBuilded = true;
            return _unitActionParameters;
        }
    }
}
