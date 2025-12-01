using UnityEngine;

public static class HelperUtilities
{
    // 验证数组是否为空，并在编辑器中给出提示
    public static void ValidateCheckEnumerableValues<T>(UnityEngine.Object target, string fieldName, T[] array)
    {
#if UNITY_EDITOR
        if (array == null || array.Length == 0)
        {
            Debug.LogWarning($"[{target.name}] {fieldName} is empty. Please fill in the required values.", target);
        }
#endif
    }
    public static void ValidateCheckEmptyString(UnityEngine.Object target, string fieldName, string value)
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogWarning($"[{target.name}] {fieldName} is empty. Please fill in the required value.", target);
        }
#endif
    }

    // 验证对象是否为 null，并在编辑器中给出提示
    public static void ValidateCheckNullValue(UnityEngine.Object target, string fieldName, UnityEngine.Object value)
    {
#if UNITY_EDITOR
        if (value == null)
        {
            Debug.LogWarning($"[{target.name}] {fieldName} is null. Please assign a valid value.", target);
        }
#endif
    }

    // 验证值是否为正数，并在编辑器中给出提示
    public static void ValidateCheckPositiveValue(UnityEngine.Object target, string fieldName, float value)
    {
#if UNITY_EDITOR
        if (value <= 0)
        {
            Debug.LogWarning($"[{target.name}] {fieldName} must be a positive value. Current value: {value}", target);
        }
#endif
    }
}