using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu( fileName = "EnemiesData", menuName = "EnemiesData" )]
public class EnemiesData : ScriptableObject
{
    private const string ENUM_NAME = "EnemyTypes";
    
    public List<EnemyData> _enemyTypes;
    
    private void OnValidate() {
        
        string filePathAndName = "Assets/Scripts/Enums/" + ENUM_NAME + ".cs";
 
        using ( StreamWriter streamWriter = new StreamWriter( filePathAndName ) )
        {
            streamWriter.WriteLine( "public enum " + ENUM_NAME );
            streamWriter.WriteLine( "{" );
            for( int i = 0; i < _enemyTypes.Count; i++ )
            {
                if(_enemyTypes[i].IncludeInEnum)
                    streamWriter.WriteLine( "\t" + _enemyTypes[i].Name + "," );
            }
            streamWriter.WriteLine( "}" );
        }
        AssetDatabase.Refresh();
    }
}
