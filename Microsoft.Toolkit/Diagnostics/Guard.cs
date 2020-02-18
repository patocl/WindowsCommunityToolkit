﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable enable

namespace Microsoft.Toolkit.Diagnostics
{
    /// <summary>
    /// Helper methods to verify conditions when running code.
    /// </summary>
    [DebuggerStepThrough]
    public static partial class Guard
    {
        /// <summary>
        /// Asserts that the input value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of reference value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is not <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull<T>(T? value, string name)
            where T : class
        {
            if (value != null)
            {
                ThrowArgumentNullException(name, $"Parameter {name} must be null");
            }
        }

        /// <summary>
        /// Asserts that the input value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of nullable value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is not <see langword="null"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull<T>(T? value, string name)
            where T : struct
        {
            if (value != null)
            {
                ThrowArgumentNullException(name, $"Parameter {name} must be null");
            }
        }

        /// <summary>
        /// Asserts that the input value is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of reference value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotNull<T>(T? value, string name)
            where T : class
        {
            if (value is null)
            {
                ThrowArgumentNullException(name, $"Parameter {name} must be not null");
            }
        }

        /// <summary>
        /// Asserts that the input value is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of nullable value type being tested.</typeparam>
        /// <param name="value">The input value to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotNull<T>(T? value, string name)
            where T : struct
        {
            if (value is null)
            {
                ThrowArgumentNullException(name, $"Parameter {name} must be not null");
            }
        }

        /// <summary>
        /// Asserts that the input value is of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not of type <typeparamref name="T"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsOfType<T>(object value, string name)
        {
            if (value.GetType() != typeof(T))
            {
                ThrowArgumentException(name, $"Parameter {name} must be of type {typeof(T)}, was {value.GetType()}");
            }
        }

        /// <summary>
        /// Asserts that the input value is of a specific type.
        /// </summary>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if the type of <paramref name="value"/> is not the same as <paramref name="type"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsOfType(object value, Type type, string name)
        {
            if (value.GetType() != type)
            {
                ThrowArgumentException(name, $"Parameter {name} must be of type {type}, was {value.GetType()}");
            }
        }

        /// <summary>
        /// Asserts that the input value can be cast to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to check the input value against.</typeparam>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can't be cast to type <typeparamref name="T"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsAssignableToType<T>(object value, string name)
        {
            if (!(value is T))
            {
                ThrowArgumentException(name, $"Parameter {name} must be assignable to type {typeof(T)}, was {value.GetType()}");
            }
        }

        /// <summary>
        /// Asserts that the input value can be cast to a specified type.
        /// </summary>
        /// <param name="value">The input <see cref="object"/> to test.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can't be cast to <paramref name="type"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsAssignableToType(object value, Type type, string name)
        {
            if (!type.IsInstanceOfType(value))
            {
                ThrowArgumentException(name, $"Parameter {name} must be assignable to type {type}, was {value.GetType()}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be equal to a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is != <paramref name="target"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsEqualTo<T>(T value, T target, string name)
            where T : notnull, IEquatable<T>
        {
            if (!value.Equals(target))
            {
                ThrowArgumentException(name, $"Parameter {name} must be == {target}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be not equal to a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is == <paramref name="target"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotEqualTo<T>(T value, T target, string name)
            where T : notnull, IEquatable<T>
        {
            if (value.Equals(target))
            {
                ThrowArgumentException(name, $"Parameter {name} must be != {target}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be a bitwise match with a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not a bitwise match for <paramref name="target"/>.</exception>
        public static void IsBitwiseEqualTo<T>(T value, T target, string name)
            where T : unmanaged
        {
            /* Include some fast paths if the input type is of size 1, 2, 4 or 8.
             * In those cases, just reinterpret the bytes as values of an integer type,
             * and compare them directly, which is much faster than having explicitly
             * loop through every single byte. This also allows for more expressive error
             * messages, since the entire input values can be expressed as hexadecimal values.
             * The conditional branches below are known at compile time by the JIT compiler,
             * so that only the right one will actually be translated into native code. */
            if (typeof(T) == typeof(byte) ||
                typeof(T) == typeof(sbyte) ||
                typeof(T) == typeof(bool))
            {
                byte valueByte = Unsafe.As<T, byte>(ref value);
                byte targetByte = Unsafe.As<T, byte>(ref target);

                if (valueByte != targetByte)
                {
                    ThrowArgumentException(name, $"Parameter {name} must is not a bitwise match, was 0x{valueByte:X2} instead of 0x{targetByte:X2}");
                }
            }
            else if (typeof(T) == typeof(ushort) ||
                     typeof(T) == typeof(short) ||
                     typeof(T) == typeof(char))
            {
                ushort valueUShort = Unsafe.As<T, ushort>(ref value);
                ushort targetUShort = Unsafe.As<T, ushort>(ref target);

                if (valueUShort != targetUShort)
                {
                    ThrowArgumentException(name, $"Parameter {name} must is not a bitwise match, was 0x{valueUShort:X4} instead of 0x{targetUShort:X4}");
                }
            }
            else if (typeof(T) == typeof(uint) ||
                     typeof(T) == typeof(int) ||
                     typeof(T) == typeof(float))
            {
                uint valueUInt = Unsafe.As<T, uint>(ref value);
                uint targetUInt = Unsafe.As<T, uint>(ref target);

                if (valueUInt != targetUInt)
                {
                    ThrowArgumentException(name, $"Parameter {name} must is not a bitwise match, was 0x{valueUInt:X8} instead of 0x{targetUInt:X8}");
                }
            }
            else if (typeof(T) == typeof(ulong) ||
                     typeof(T) == typeof(long) ||
                     typeof(T) == typeof(double))
            {
                ulong valueULong = Unsafe.As<T, ulong>(ref value);
                ulong targetULong = Unsafe.As<T, ulong>(ref target);

                if (valueULong != targetULong)
                {
                    ThrowArgumentException(name, $"Parameter {name} must is not a bitwise match, was 0x{valueULong:X16} instead of 0x{targetULong:X16}");
                }
            }
            else
            {
                ref byte valueRef = ref Unsafe.As<T, byte>(ref value);
                ref byte targetRef = ref Unsafe.As<T, byte>(ref target);
                int bytesCount = Unsafe.SizeOf<T>();

                for (int i = 0; i < bytesCount; i++)
                {
                    byte valueByte = Unsafe.Add(ref valueRef, i);
                    byte targetByte = Unsafe.Add(ref targetRef, i);

                    if (valueByte != targetByte)
                    {
                        ThrowArgumentException(name, $"Parameter {name} is not a bitwise match (byte #{i} was 0x{valueByte:X2} instead of 0x{targetByte:X2})");
                    }
                }
            }
        }

        /// <summary>
        /// Asserts that the input value must be the same instance as the target value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is not the same instance as <paramref name="target"/>.</exception>
        /// <remarks>The method is generic to prevent using it with value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsReferenceEqualTo<T>(T value, T target, string name)
            where T : class
        {
            if (!ReferenceEquals(value, target))
            {
                ThrowArgumentException(name, $"Parameter {name} must be the same instance as the target object");
            }
        }

        /// <summary>
        /// Asserts that the input value must not be the same instance as the target value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="target">The target <typeparamref name="T"/> value to test for.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is the same instance as <paramref name="target"/>.</exception>
        /// <remarks>The method is generic to prevent using it with value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsReferenceNotEqualTo<T>(T value, T target, string name)
            where T : class
        {
            if (ReferenceEquals(value, target))
            {
                ThrowArgumentException(name, $"Parameter {name} must not be the same instance as the target object");
            }
        }

        /// <summary>
        /// Asserts that the input value must be <see langword="true"/>.
        /// </summary>
        /// <param name="value">The input <see cref="bool"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="false"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsTrue(bool value, string name)
        {
            if (!value)
            {
                ThrowArgumentException(name, $"Parameter {name} must be true, was false");
            }
        }

        /// <summary>
        /// Asserts that the input value must be <see langword="false"/>.
        /// </summary>
        /// <param name="value">The input <see cref="bool"/> to test.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="true"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsFalse(bool value, string name)
        {
            if (value)
            {
                ThrowArgumentException(name, $"Parameter {name} must be false, was true");
            }
        }

        /// <summary>
        /// Asserts that the input value must be less than a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="max">The exclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is >= <paramref name="max"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsLessThan<T>(T value, T max, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(max) >= 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be < {max}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be less than or equal to a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="maximum">The inclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is > <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsLessThanOrEqualTo<T>(T value, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(maximum) > 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be <= {maximum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be greater than a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The exclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is &lt;= <paramref name="minimum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsGreaterThan<T>(T value, T minimum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) <= 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be > {minimum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be greater than or equal to a specified value.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The inclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is &lt; <paramref name="minimum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsGreaterThanOrEqualTo<T>(T value, T minimum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) < 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be >= {minimum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be in a given range.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The inclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="maximum">The exclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is &lt; <paramref name="minimum"/> or >= <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsInRange<T>(T value, T minimum, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) >= 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be >= {minimum} and < {maximum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must not be in a given range.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The inclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="maximum">The exclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is >= <paramref name="minimum"/> or &lt; <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotInRange<T>(T value, T minimum, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) >= 0 || value.CompareTo(maximum) < 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be < {minimum} or >= {maximum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be in a given interval.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The exclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="maximum">The exclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is &lt;= <paramref name="minimum"/> or >= <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsBetween<T>(T value, T minimum, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) <= 0 || value.CompareTo(maximum) >= 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be > {minimum} and < {maximum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must not be in a given interval.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The exclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="maximum">The exclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is > <paramref name="minimum"/> or &lt; <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotBetween<T>(T value, T minimum, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) > 0 || value.CompareTo(maximum) < 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be <= {minimum} or >= {maximum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must be in a given interval.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The inclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="maximum">The inclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is &lt; <paramref name="minimum"/> or > <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsBetweenOrEqualTo<T>(T value, T minimum, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be >= {minimum} and <= {maximum}, was {value}");
            }
        }

        /// <summary>
        /// Asserts that the input value must not be in a given interval.
        /// </summary>
        /// <typeparam name="T">The type of input values to compare.</typeparam>
        /// <param name="value">The input <typeparamref name="T"/> value to test.</param>
        /// <param name="minimum">The inclusive minimum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="maximum">The inclusive maximum <typeparamref name="T"/> value that is accepted.</param>
        /// <param name="name">The name of the input parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is >= <paramref name="minimum"/> or &lt;= <paramref name="maximum"/>.</exception>
        /// <remarks>The method is generic to avoid boxing the parameters, if they are value types.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotBetweenOrEqualTo<T>(T value, T minimum, T maximum, string name)
            where T : notnull, IComparable<T>
        {
            if (value.CompareTo(minimum) >= 0 || value.CompareTo(maximum) <= 0)
            {
                ThrowArgumentOutOfRangeException(name, $"Parameter {name} must be < {minimum} or > {maximum}, was {value}");
            }
        }
    }
}