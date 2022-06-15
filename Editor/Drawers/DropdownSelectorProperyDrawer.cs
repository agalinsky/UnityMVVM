using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace UnityMVVM.Editor
{
    [CustomPropertyDrawer(typeof(DropdownSelectorAttribute))]
    public class DropdownSelectorPropertyDrawer : PropertyDrawer
    {
        private static IReadOnlyList<string> _tagItems;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                var attrib = this.attribute as DropdownSelectorAttribute;

                if (attrib.UseDefaultTagFieldDrawer)
                {
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                }
                else
                {
                    List<string> tagList = new List<string>();
                    tagList.Add("None");

                    // Reflection logic to find all tags
                    if (_tagItems == null)
                    {
                        var tagItems = new List<string>();

                        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                        foreach (var assembly in assemblies)
                        {
                            var types = assembly.GetTypes();

                            foreach (var type in types)
                            {
                                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                                foreach (var field in fields)
                                {
                                    if (field.IsDefined(typeof(DropdownItemAttribute), true))
                                    {
                                        var value = field.GetValue(null) as string;

                                        if (string.IsNullOrEmpty(field.GetValue(null) as string) == false)
                                        {
                                            tagItems.Add(value);
                                        }
                                    }
                                }
                            }
                        }

                        _tagItems = tagItems;
                    }

                    tagList.AddRange(_tagItems);

                    string propertyString = property.stringValue;
                    int index = -1;
                    if (propertyString == "")
                    {
                        //The tag is empty
                        index = 0; //first index is the special <None> entry
                    }
                    else
                    {
                        //check if there is an entry that matches the entry and get the index
                        //we skip index 0 as that is a special custom case
                        for (int i = 1; i < tagList.Count; i++)
                        {
                            if (tagList[i] == propertyString)
                            {
                                index = i;
                                break;
                            }
                        }
                    }

                    //Draw the popup box with the current selected index
                    index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                    //Adjust the actual string value of the property based on the selection
                    if (index == 0)
                    {
                        property.stringValue = "";
                    }
                    else if (index >= 1)
                    {
                        property.stringValue = tagList[index];
                    }
                    else
                    {
                        property.stringValue = "";
                    }
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
