using System;
using System.Globalization;

namespace Dirac.Math
{
    /// <summary>
    ///		Summary description for Quaternion.
    /// </summary>
    public struct Quaternion
    {
        #region Private member variables and constants

        private const float EPSILON = 1e-03f;

        public float w, x, y, z;

        private static readonly Quaternion identityQuat = new Quaternion(1.0f, 0.0f, 0.0f, 0.0f);
        private static readonly Quaternion zeroQuat = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        private static readonly int[] next = new int[3] {
		                                                	1, 2, 0
		                                                };

        #endregion Private member variables and constants

        #region Constructors

        //		public Quaternion()
        //		{
        //			this.w = 1.0f;
        //		}

        /// <summary>
        ///		Creates a new Quaternion.
        /// </summary>
        public Quaternion(float w, float x, float y, float z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #endregion Constructors

        #region Operator Overloads + CLS compliant method equivalents

        /// <summary>
        /// Used to multiply 2 Quaternions together.
        /// </summary>
        /// <remarks>
        ///		Quaternion multiplication is not communative in most cases.
        ///		i.e. p*q != q*p
        /// </remarks>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            return left * right;
        }

        /// <summary>
        /// Used to multiply 2 Quaternions together.
        /// </summary>
        /// <remarks>
        ///		Quaternion multiplication is not communative in most cases.
        ///		i.e. p*q != q*p
        /// </remarks>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            Quaternion q = new Quaternion();

            q.w = left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z;
            q.x = left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y;
            q.y = left.w * right.y + left.y * right.w + left.z * right.x - left.x * right.z;
            q.z = left.w * right.z + left.z * right.w + left.x * right.y - left.y * right.x;

            /*
            return new Quaternion
                (
                left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z,
                left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y,
                left.w * right.y + left.y * right.w + left.z * right.x - left.x * right.z,
                left.w * right.z + left.z * right.w + left.x * right.y - left.y * right.x
                ); */

            return q;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="quat"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Multiply(Quaternion quat, Vector3 vector)
        {
            return quat * vector;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="quat"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 operator *(Quaternion quat, Vector3 vector)
        {
            // nVidia SDK implementation
            Vector3 uv, uuv;
            Vector3 qvec = new Vector3(quat.x, quat.y, quat.z);

            uv = qvec.Cross(vector);
            uuv = qvec.Cross(uv);
            uv *= (2.0f * quat.w);
            uuv *= 2.0f;

            return vector + uv + uuv;

            // get the rotation matrix of the Quaternion and multiply it times the vector
            //return quat.ToRotationMatrix() * vector;
        }

        /// <summary>
        /// Used when a float value is multiplied by a Quaternion.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion Multiply(float scalar, Quaternion right)
        {
            return scalar * right;
        }

        /// <summary>
        /// Used when a float value is multiplied by a Quaternion.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator *(float scalar, Quaternion right)
        {
            return new Quaternion(scalar * right.w, scalar * right.x, scalar * right.y, scalar * right.z);
        }

        /// <summary>
        /// Used when a Quaternion is multiplied by a float value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Quaternion Multiply(Quaternion left, float scalar)
        {
            return left * scalar;
        }

        /// <summary>
        /// Used when a Quaternion is multiplied by a float value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion left, float scalar)
        {
            return new Quaternion(scalar * left.w, scalar * left.x, scalar * left.y, scalar * left.z);
        }

        /// <summary>
        /// Used when a Quaternion is added to another Quaternion.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            return left + right;
        }

        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            return left - right;
        }

        /// <summary>
        /// Used when a Quaternion is added to another Quaternion.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            return new Quaternion(left.w + right.w, left.x + right.x, left.y + right.y, left.z + right.z);
        }

        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            return new Quaternion(left.w - right.w, left.x - right.x, left.y - right.y, left.z - right.z);
        }

        /// <summary>
        ///     Negates a Quaternion, which simply returns a new Quaternion
        ///     with all components negated.
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Quaternion operator -(Quaternion right)
        {
            return new Quaternion(-right.w, -right.x, -right.y, -right.z);
        }

        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return (left.w == right.w && left.x == right.x && left.y == right.y && left.z == right.z);
        }

        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return !(left == right);
        }

        #endregion Operator Overloads + CLS compliant method equivalents

        #region Properties

        /// <summary>
        ///    An Identity Quaternion.
        /// </summary>
        public static Quaternion Identity { get { return identityQuat; } }

        /// <summary>
        ///    A Quaternion with all elements set to 0.0f;
        /// </summary>
        public static Quaternion Zero { get { return zeroQuat; } }

        /// <summary>
        ///		Squared 'length' of this quaternion.
        /// </summary>
        public float Norm { get { return x * x + y * y + z * z + w * w; } }

        /// <summary>
        ///    Local X-axis portion of this rotation.
        /// </summary>
        public Vector3 XAxis
        {
            get
            {
                float fTx = 2.0f * x;
                float fTy = 2.0f * y;
                float fTz = 2.0f * z;
                float fTwy = fTy * w;
                float fTwz = fTz * w;
                float fTxy = fTy * x;
                float fTxz = fTz * x;
                float fTyy = fTy * y;
                float fTzz = fTz * z;

                return new Vector3(1.0f - (fTyy + fTzz), fTxy + fTwz, fTxz - fTwy);
            }
        }

        /// <summary>
        ///    Local Y-axis portion of this rotation.
        /// </summary>
        public Vector3 YAxis
        {
            get
            {
                float fTx = 2.0f * x;
                float fTy = 2.0f * y;
                float fTz = 2.0f * z;
                float fTwx = fTx * w;
                float fTwz = fTz * w;
                float fTxx = fTx * x;
                float fTxy = fTy * x;
                float fTyz = fTz * y;
                float fTzz = fTz * z;

                return new Vector3(fTxy - fTwz, 1.0f - (fTxx + fTzz), fTyz + fTwx);
            }
        }

        /// <summary>
        ///    Local Z-axis portion of this rotation.
        /// </summary>
        public Vector3 ZAxis
        {
            get
            {
                float fTx = 2.0f * x;
                float fTy = 2.0f * y;
                float fTz = 2.0f * z;
                float fTwx = fTx * w;
                float fTwy = fTy * w;
                float fTxx = fTx * x;
                float fTxz = fTz * x;
                float fTyy = fTy * y;
                float fTyz = fTz * y;

                return new Vector3(fTxz + fTwy, fTyz - fTwx, 1.0f - (fTxx + fTyy));
            }
        }

        public float PitchInDegrees { get { return Utility.RadiansToDegrees(Pitch); } set { Pitch = Utility.DegreesToRadians(value); } }

        public float YawInDegrees { get { return Utility.RadiansToDegrees(Yaw); } set { Yaw = Utility.DegreesToRadians(value); } }

        public float RollInDegrees { get { return Utility.RadiansToDegrees(Roll); } set { Roll = Utility.DegreesToRadians(value); } }

        public float Pitch
        {
            set
            {
                float pitch, yaw, roll;
                ToEulerAngles(out pitch, out yaw, out roll);
                this = FromEulerAngles(value, yaw, roll);
            }
            get
            {
                float test = x * y + z * w;
                if (Utility.Abs(test) > 0.499f) // singularity at north and south pole
                {
                    return 0f;
                }
                return (float)Utility.ATan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            }
        }

        public float Yaw
        {
            set
            {
                float pitch, yaw, roll;
                ToEulerAngles(out pitch, out yaw, out roll);
                this = FromEulerAngles(pitch, value, roll);
            }
            get
            {
                float test = x * y + z * w;
                if (Utility.Abs(test) > 0.499f) // singularity at north and south pole
                {
                    return Utility.Sign(test) * 2 * Utility.ATan2(x, w);
                }
                return Utility.ATan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            }
        }

        public float Roll
        {
            set
            {
                float pitch, yaw, roll;
                ToEulerAngles(out pitch, out yaw, out roll);
                this = FromEulerAngles(pitch, yaw, value);
            }
            get
            {
                float test = x * y + z * w;
                if (Utility.Abs(test) > 0.499f) // singularity at north and south pole
                {
                    return Utility.Sign(test) * Utility.PI / 2;
                }
                return (float)Utility.ASin(2 * test);
            }
        }

        #endregion Properties

        #region Static methods

        public static Quaternion Slerp(float time, Quaternion quatA, Quaternion quatB)
        {
            return Slerp(time, quatA, quatB, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="time"></param>
        /// <param name="quatA"></param>
        /// <param name="quatB"></param>
        /// <param name="useShortestPath"></param>
        /// <returns></returns>
        public static Quaternion Slerp(float time, Quaternion quatA, Quaternion quatB, bool useShortestPath)
        {
            float cos = quatA.Dot(quatB);

            float angle = (float)Utility.ACos(cos);

            if (Utility.Abs(angle) < EPSILON)
            {
                return quatA;
            }

            float sin = Utility.Sin(angle);
            float inverseSin = 1.0f / sin;
            float coeff0 = Utility.Sin((1.0f - time) * angle) * inverseSin;
            float coeff1 = Utility.Sin(time * angle) * inverseSin;

            Quaternion result;

            if (cos < 0.0f && useShortestPath)
            {
                coeff0 = -coeff0;
                // taking the complement requires renormalisation
                Quaternion t = coeff0 * quatA + coeff1 * quatB;
                t.Normalize();
                result = t;
            }
            else
            {
                result = (coeff0 * quatA + coeff1 * quatB);
            }

            return result;
        }

        /// <overloads><summary>
        /// normalised linear interpolation - faster but less accurate (non-constant rotation velocity)
        /// </summary>
        /// <param name="fT"></param>
        /// <param name="rkP"></param>
        /// <param name="rkQ"></param>
        /// <returns></returns>
        /// </overloads>
        public static Quaternion Nlerp(float fT, Quaternion rkP, Quaternion rkQ)
        {
            return Nlerp(fT, rkP, rkQ, false);
        }

        /// <param name="shortestPath"></param>
        public static Quaternion Nlerp(float fT, Quaternion rkP, Quaternion rkQ, bool shortestPath)
        {
            Quaternion result;
            float fCos = rkP.Dot(rkQ);
            if (fCos < 0.0f && shortestPath)
            {
                result = rkP + fT * ((-rkQ) - rkP);
            }
            else
            {
                result = rkP + fT * (rkQ - rkP);
            }
            result.Normalize();
            return result;
        }

        /// <summary>
        /// Creates a Quaternion from a supplied angle and axis.
        /// </summary>
        /// <param name="angle">Value of an angle in radians.</param>
        /// <param name="axis">Arbitrary axis vector.</param>
        /// <returns></returns>
        public static Quaternion FromAngleAxis(float angle, Vector3 axis)
        {
            Quaternion quat = new Quaternion();

            float halfAngle = 0.5f * angle;
            float sin = Utility.Sin(halfAngle);

            quat.w = Utility.Cos(halfAngle);
            quat.x = sin * axis.x;
            quat.y = sin * axis.y;
            quat.z = sin * axis.z;

            return quat;
        }

        public static Quaternion Squad(float t, Quaternion p, Quaternion a, Quaternion b, Quaternion q)
        {
            return Squad(t, p, a, b, q, false);
        }

        /// <summary>
        ///		Performs spherical quadratic interpolation.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion Squad(float t, Quaternion p, Quaternion a, Quaternion b, Quaternion q, bool useShortestPath)
        {
            float slerpT = 2.0f * t * (1.0f - t);

            // use spherical linear interpolation
            Quaternion slerpP = Slerp(t, p, q, useShortestPath);
            Quaternion slerpQ = Slerp(t, a, b);

            // run another Slerp on the results of the first 2, and return the results
            return Slerp(slerpT, slerpP, slerpQ);
        }

        #endregion Static methods

        #region Public methods

        #region Euler Angles

        public Vector3 ToEulerAnglesInDegrees()
        {
            float pitch, yaw, roll;
            ToEulerAngles(out pitch, out yaw, out roll);
            return new Vector3(Utility.RadiansToDegrees(pitch), Utility.RadiansToDegrees(yaw), Utility.RadiansToDegrees(roll));
        }

        public Vector3 ToEulerAngles()
        {
            float pitch, yaw, roll;
            ToEulerAngles(out pitch, out yaw, out roll);
            return new Vector3(pitch, yaw, roll);
        }

        public void ToEulerAnglesInDegrees(out float pitch, out float yaw, out float roll)
        {
            ToEulerAngles(out pitch, out yaw, out roll);
            pitch = Utility.RadiansToDegrees(pitch);
            yaw = Utility.RadiansToDegrees(yaw);
            roll = Utility.RadiansToDegrees(roll);
        }

        public void ToEulerAngles(out float pitch, out float yaw, out float roll)
        {
            float halfPi = Utility.PI / 2;
            float test = x * y + z * w;
            if (test > 0.499f)
            {
                // singularity at north pole
                yaw = 2 * Utility.ATan2(x, w);
                roll = halfPi;
                pitch = 0;
            }
            else if (test < -0.499f)
            {
                // singularity at south pole
                yaw = -2 * Utility.ATan2(x, w);
                roll = -halfPi;
                pitch = 0;
            }
            else
            {
                float sqx = x * x;
                float sqy = y * y;
                float sqz = z * z;
                yaw = Utility.ATan2(2 * y * w - 2 * x * z, 1 - 2 * sqy - 2 * sqz);
                roll = (float)Utility.ASin(2 * test);
                pitch = Utility.ATan2(2 * x * w - 2 * y * z, 1 - 2 * sqx - 2 * sqz);
            }

            if (pitch <= float.Epsilon)
            {
                pitch = 0f;
            }
            if (yaw <= float.Epsilon)
            {
                yaw = 0f;
            }
            if (roll <= float.Epsilon)
            {
                roll = 0f;
            }
        }

        public static Quaternion FromEulerAnglesInDegrees(float pitch, float yaw, float roll)
        {
            return FromEulerAngles(Utility.DegreesToRadians(pitch), Utility.DegreesToRadians(yaw), Utility.DegreesToRadians(roll));
        }

        /// <summary>
        /// Combines the euler angles in the order yaw, pitch, roll to create a rotation quaternion
        /// </summary>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <param name="roll"></param>
        /// <returns></returns>
        public static Quaternion FromEulerAngles(float pitch, float yaw, float roll)
        {
            return Quaternion.FromAngleAxis(yaw, Vector3.UnitY)
                   * Quaternion.FromAngleAxis(pitch, Vector3.UnitX)
                   * Quaternion.FromAngleAxis(roll, Vector3.UnitZ);

            /*TODO: Debug
            //Equation from http://www.euclideanspace.com/maths/geometry/rotations/conversions/eulerToQuaternion/index.htm
            //heading

            float c1 = (float)Math.Cos(yaw/2);
            float s1 = (float)Math.Sin(yaw/2);
            //attitude
            float c2 = (float)Math.Cos(roll/2);
            float s2 = (float)Math.Sin(roll/2);
            //bank
            float c3 = (float)Math.Cos(pitch/2);
            float s3 = (float)Math.Sin(pitch/2);
            float c1c2 = c1*c2;
            float s1s2 = s1*s2;

            float w =c1c2*c3 - s1s2*s3;
            float x =c1c2*s3 + s1s2*c3;
            float y =s1*c2*c3 + c1*s2*s3;
            float z =c1*s2*c3 - s1*c2*s3;
            return new Quaternion(w,x,y,z);*/
        }

        #endregion Euler Angles

        /// <summary>
        /// Performs a Dot Product operation on 2 Quaternions.
        /// </summary>
        /// <param name="quat"></param>
        /// <returns></returns>
        public float Dot(Quaternion quat)
        {
            return this.w * quat.w + this.x * quat.x + this.y * quat.y + this.z * quat.z;
        }

        /// <summary>
        ///		Normalizes elements of this quaterion to the range [0,1].
        /// </summary>
        public void Normalize()
        {
            float factor = 1.0f / Utility.Sqrt(this.Norm);

            w = w * factor;
            x = x * factor;
            y = y * factor;
            z = z * factor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public void ToAngleAxis(ref float angle, ref Vector3 axis)
        {
            // The quaternion representing the rotation is
            //   q = cos(A/2)+sin(A/2)*(x*i+y*j+z*k)

            float sqrLength = x * x + y * y + z * z;

            if (sqrLength > 0.0f)
            {
                angle = 2.0f * (float)Utility.ACos(w);
                float invLength = Utility.InvSqrt(sqrLength);
                axis.x = x * invLength;
                axis.y = y * invLength;
                axis.z = z * invLength;
            }
            else
            {
                angle = 0.0f;
                axis.x = 1.0f;
                axis.y = 0.0f;
                axis.z = 0.0f;
            }
        }

        /// <summary>
        /// Gets a 3x3 rotation matrix from this Quaternion.
        /// </summary>
        /// <returns></returns>
        public Matrix3 ToRotationMatrix()
        {
            Matrix3 rotation = new Matrix3();

            float tx = 2.0f * this.x;
            float ty = 2.0f * this.y;
            float tz = 2.0f * this.z;
            float twx = tx * this.w;
            float twy = ty * this.w;
            float twz = tz * this.w;
            float txx = tx * this.x;
            float txy = ty * this.x;
            float txz = tz * this.x;
            float tyy = ty * this.y;
            float tyz = tz * this.y;
            float tzz = tz * this.z;

            rotation.m00 = 1.0f - (tyy + tzz);
            rotation.m01 = txy - twz;
            rotation.m02 = txz + twy;
            rotation.m10 = txy + twz;
            rotation.m11 = 1.0f - (txx + tzz);
            rotation.m12 = tyz - twx;
            rotation.m20 = txz - twy;
            rotation.m21 = tyz + twx;
            rotation.m22 = 1.0f - (txx + tyy);

            return rotation;
        }

        /// <summary>
        /// Computes the inverse of a Quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion Inverse()
        {
            float norm = this.w * this.w + this.x * this.x + this.y * this.y + this.z * this.z;
            if (norm > 0.0f)
            {
                float inverseNorm = 1.0f / norm;
                return new Quaternion(this.w * inverseNorm, -this.x * inverseNorm, -this.y * inverseNorm, -this.z * inverseNorm);
            }
            else
            {
                // return an invalid result to flag the error
                return Quaternion.Zero;
            }
        }

        /// <summary>
        ///   Variant of Inverse() that is only valid for unit quaternions.
        /// </summary>
        /// <returns></returns>
        public Quaternion UnitInverse { get { return new Quaternion(w, -x, -y, -z); } }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        public void ToAxes(out Vector3 xAxis, out Vector3 yAxis, out Vector3 zAxis)
        {
            xAxis = new Vector3();
            yAxis = new Vector3();
            zAxis = new Vector3();

            Matrix3 rotation = this.ToRotationMatrix();

            xAxis.x = rotation.m00;
            xAxis.y = rotation.m10;
            xAxis.z = rotation.m20;

            yAxis.x = rotation.m01;
            yAxis.y = rotation.m11;
            yAxis.z = rotation.m21;

            zAxis.x = rotation.m02;
            zAxis.y = rotation.m12;
            zAxis.z = rotation.m22;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="zAxis"></param>
        public static Quaternion FromAxes(Vector3 xAxis, Vector3 yAxis, Vector3 zAxis)
        {
            Matrix3 rotation = new Matrix3();

            rotation.m00 = xAxis.x;
            rotation.m10 = xAxis.y;
            rotation.m20 = xAxis.z;

            rotation.m01 = yAxis.x;
            rotation.m11 = yAxis.y;
            rotation.m21 = yAxis.z;

            rotation.m02 = zAxis.x;
            rotation.m12 = zAxis.y;
            rotation.m22 = zAxis.z;

            // set this quaternions values from the rotation matrix built
            return FromRotationMatrix(rotation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="matrix"></param>
        public static Quaternion FromRotationMatrix(Matrix3 matrix)
        {
            // Algorithm in Ken Shoemake's article in 1987 SIGGRAPH course notes
            // article "Quaternion Calculus and Fast Animation".

            Quaternion result = Quaternion.Zero;

            float trace = matrix.m00 + matrix.m11 + matrix.m22;

            float root = 0.0f;

            if (trace > 0.0f)
            {
                // |this.w| > 1/2, may as well choose this.w > 1/2
                root = Utility.Sqrt(trace + 1.0f); // 2w
                result.w = 0.5f * root;

                root = 0.5f / root; // 1/(4w)

                result.x = (matrix.m21 - matrix.m12) * root;
                result.y = (matrix.m02 - matrix.m20) * root;
                result.z = (matrix.m10 - matrix.m01) * root;
            }
            else
            {
                // |result.w| <= 1/2

                int i = 0;
                if (matrix.m11 > matrix.m00)
                {
                    i = 1;
                }
                if (matrix.m22 > matrix[i, i])
                {
                    i = 2;
                }

                int j = next[i];
                int k = next[j];

                root = Utility.Sqrt(matrix[i, i] - matrix[j, j] - matrix[k, k] + 1.0f);

                unsafe
                {
                    float* apkQuat = &result.x;

                    apkQuat[i] = 0.5f * root;
                    root = 0.5f / root;

                    result.w = (matrix[k, j] - matrix[j, k]) * root;

                    apkQuat[j] = (matrix[j, i] + matrix[i, j]) * root;
                    apkQuat[k] = (matrix[k, i] + matrix[i, k]) * root;
                }
            }

            return result;
        }

        /// <summary>
        ///		Calculates the logarithm of a Quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion Log()
        {
            // BLACKBOX: Learn this
            // If q = cos(A)+sin(A)*(x*i+y*j+z*k) where (x,y,z) is unit length, then
            // log(q) = A*(x*i+y*j+z*k).  If sin(A) is near zero, use log(q) =
            // sin(A)*(x*i+y*j+z*k) since sin(A)/A has limit 1.

            // start off with a zero quat
            Quaternion result = Quaternion.Zero;

            if (Utility.Abs(w) < 1.0f)
            {
                float angle = (float)Utility.ACos(w);
                float sin = Utility.Sin(angle);

                if (Utility.Abs(sin) >= EPSILON)
                {
                    float coeff = angle / sin;
                    result.x = coeff * x;
                    result.y = coeff * y;
                    result.z = coeff * z;
                }
                else
                {
                    result.x = x;
                    result.y = y;
                    result.z = z;
                }
            }

            return result;
        }

        /// <summary>
        ///		Calculates the Exponent of a Quaternion.
        /// </summary>
        /// <returns></returns>
        public Quaternion Exp()
        {
            // If q = A*(x*i+y*j+z*k) where (x,y,z) is unit length, then
            // exp(q) = cos(A)+sin(A)*(x*i+y*j+z*k).  If sin(A) is near zero,
            // use exp(q) = cos(A)+A*(x*i+y*j+z*k) since A/sin(A) has limit 1.

            float angle = Utility.Sqrt(x * x + y * y + z * z);
            float sin = Utility.Sin(angle);

            // start off with a zero quat
            Quaternion result = Quaternion.Zero;

            result.w = Utility.Cos(angle);

            if (Utility.Abs(sin) >= EPSILON)
            {
                float coeff = sin / angle;

                result.x = coeff * x;
                result.y = coeff * y;
                result.z = coeff * z;
            }
            else
            {
                result.x = x;
                result.y = y;
                result.z = z;
            }

            return result;
        }

        #endregion Public methods

        #region Object overloads

        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of
        ///		a Quaternion.
        /// </summary>
        /// <returns>A string representation of a Quaternion.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Quaternion({0}, {1}, {2}, {3})", this.w, this.x, this.y, this.z);
        }

        public override int GetHashCode()
        {
            return (int)x ^ (int)y ^ (int)z ^ (int)w;
        }

        public override bool Equals(object obj)
        {
            Quaternion quat = (Quaternion)obj;

            return quat == this;
        }

        public bool Equals(Quaternion rhs, float tolerance)
        {
            float fCos = Dot(rhs);
            float angle = (float)Utility.ACos(fCos);

            return Utility.Abs(angle) <= tolerance;
        }

        #endregion Object overloads

        #region Parse from string

        /*public Quaternion Parse(string quat)
        {
            // the format is "Quaternion(w, x, y, z)"
            if (!quat.StartsWith("Quaternion("))
            {
                throw new FormatException();
            }

            string[] values = quat.Substring(11).TrimEnd(')').Split(',');

            return new Quaternion(float.Parse(values[0], CultureInfo.InvariantCulture),
                                   float.Parse(values[1], CultureInfo.InvariantCulture),
                                   float.Parse(values[2], CultureInfo.InvariantCulture),
                                   float.Parse(values[3], CultureInfo.InvariantCulture));
        }*/

        #endregion Parse from string
    }
}