﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ultima5Redux.Properties {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Ultima5Redux.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string TileData {
            get {
                return ResourceManager.GetString("TileData", resourceCulture);
            }
        }
        
        internal static string TileOverrides {
            get {
                return ResourceManager.GetString("TileOverrides", resourceCulture);
            }
        }
        
        internal static string InventoryDetails {
            get {
                return ResourceManager.GetString("InventoryDetails", resourceCulture);
            }
        }
        
        internal static string ShoppeKeeperMap {
            get {
                return ResourceManager.GetString("ShoppeKeeperMap", resourceCulture);
            }
        }
        
        internal static string CombatMaps {
            get {
                return ResourceManager.GetString("CombatMaps", resourceCulture);
            }
        }
        
        internal static string AdditionalEnemyFlags {
            get {
                return ResourceManager.GetString("AdditionalEnemyFlags", resourceCulture);
            }
        }
        
        internal static string MagicDefinitions {
            get {
                return ResourceManager.GetString("MagicDefinitions", resourceCulture);
            }
        }
        
        internal static byte[] InitGam {
            get {
                object obj = ResourceManager.GetObject("InitGam", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] InitOol {
            get {
                object obj = ResourceManager.GetObject("InitOol", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static string CustomDialogue {
            get {
                return ResourceManager.GetString("CustomDialogue", resourceCulture);
            }
        }
        
        internal static byte[] BritOol {
            get {
                object obj = ResourceManager.GetObject("BritOol", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] UnderOol {
            get {
                object obj = ResourceManager.GetObject("UnderOol", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
