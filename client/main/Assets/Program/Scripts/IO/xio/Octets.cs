
using System;
using System.Text;
using System.Runtime.Serialization;

public class Octets
{
     private byte[]         buffer = null;
     private int            count = 0;
     private static String DEFAULT_CHARSET = "ISO-8859-1";
  
     private byte[] roundup(int paramInt)
     {
         int i = 16;
         while (paramInt > i)
             i <<= 1;
         return new byte[i];
     }

     public void reserve(int paramInt)
     {
         if (this.buffer == null)
         {
             this.buffer = roundup(paramInt);
         }
         else if (paramInt > this.buffer.Length)
         {
             byte[] arrayOfByte = roundup(paramInt);
             Array.Copy(this.buffer,0, arrayOfByte, 0, this.count);
             this.buffer = arrayOfByte;
         }
     }

     public Octets replace(byte[] paramArrayOfByte, int paramInt1, int paramInt2)
     {
         reserve(paramInt2);
         Array.Copy(paramArrayOfByte, paramInt1, this.buffer, 0, paramInt2);
         this.count = paramInt2;
         return this;
     }

     public Octets replace(Octets paramOctets, int paramInt1, int paramInt2)
     {
         return replace(paramOctets.buffer, paramInt1, paramInt2);
     }

     public Octets replace(byte[] paramArrayOfByte)
     {
         return replace(paramArrayOfByte, 0, paramArrayOfByte.Length );
     }

     public Octets replace(Octets paramOctets)
     {
         return replace(paramOctets.buffer, 0, paramOctets.count );
     }

     public Octets()
     {
         reserve(128);
     }

     public Octets(int paramInt)
     {
         reserve(paramInt);
     }

     public Octets(Octets paramOctets)
     {
         replace(paramOctets);
     }

     public Octets(byte[] paramArrayOfByte)
     {
         replace(paramArrayOfByte);
     }

     private Octets(byte[] paramArrayOfByte, int paramInt)
     {
         this.buffer = paramArrayOfByte;
         this.count = paramInt;
     }

     public static Octets wrap(byte[] paramArrayOfByte, int paramInt)
     {
         return new Octets(paramArrayOfByte, paramInt);
     }

     public static Octets wrap(byte[] paramArrayOfByte)
     {
         return wrap( paramArrayOfByte, paramArrayOfByte.Length );
     }

     public static Octets wrap( string paramString1, string paramString2 )
     {
         try
         {
             byte[] _buff = Encoding.Unicode.GetBytes(paramString1);
             return wrap(_buff);
         }
         catch (System.Exception ex)
         {
             throw new SerializationException(ex.Message);
         }
     }

     public Octets(byte[] paramArrayOfByte, int paramInt1, int paramInt2)
     {
         replace(paramArrayOfByte, paramInt1, paramInt2);
     }

     public Octets(Octets paramOctets, int paramInt1, int paramInt2)
     {
         replace(paramOctets, paramInt1, paramInt2);
     }

     public Octets resize(int paramInt)
     {
         reserve(paramInt);
         this.count = paramInt;
         return this;
     }

     public int size()
     {
         return this.count;
     }

     public int capacity()
     {
         return this.buffer.Length;
     }

     public Octets clear()
     {
         this.count = 0;
         return this;
     }

     public Octets swap(Octets paramOctets)
     {
         int i = this.count;
         this.count = paramOctets.count;
         paramOctets.count = i;
         byte[] arrayOfByte = paramOctets.buffer;
         paramOctets.buffer = this.buffer;
         this.buffer = arrayOfByte;
         return this;
     }

     public Octets push_back(byte paramByte)
     {
         reserve(this.count + 1);
         this.buffer[(this.count++)] = paramByte;
         return this;
     }

     public Octets erase(int paramInt1, int paramInt2)
     {
         Array.Copy(this.buffer, paramInt2, this.buffer, paramInt1, this.count - paramInt2);
         this.count -= paramInt2 - paramInt1;
         return this;
     }

     public Octets insert(int paramInt1, byte[] paramArrayOfByte, int paramInt2, int paramInt3)
     {
         reserve(this.count + paramInt3);
         Array.Copy(this.buffer, paramInt1, this.buffer, paramInt1 + paramInt3, this.count - paramInt1);
         Array.Copy(paramArrayOfByte, paramInt2, this.buffer, paramInt1, paramInt3);
         this.count += paramInt3;
         return this;
     }

     public Octets insert(int paramInt1, Octets paramOctets, int paramInt2, int paramInt3)
     {
         return insert(paramInt1, paramOctets.buffer, paramInt2, paramInt3);
     }

     public Octets insert(int paramInt, byte[] paramArrayOfByte)
     {
         return insert(paramInt, paramArrayOfByte, 0, paramArrayOfByte.Length);
     }

     public Octets insert(int paramInt, Octets paramOctets)
     {
         return insert(paramInt, paramOctets.buffer, 0, paramOctets.size());
     }

     public object clone()
     {
         return new Octets(this);
     }

     public int compareTo(Octets paramOctets)
     {
         int i = this.count - paramOctets.count;
         if (i != 0)
             return i;
         byte[] arrayOfByte1 = this.buffer;
         byte[] arrayOfByte2 = paramOctets.buffer;
         for (int j = 0; j < this.count; j++)
         {
             int k = arrayOfByte1[j] - arrayOfByte2[j];
             if (k != 0)
                 return k;
         }
         return 0;
     }

     public int compareTo(object paramObject)
     {
         return compareTo((Octets)paramObject);
     }

     public bool equals(object paramObject)
     {
         if (this == paramObject)
             return true;
         return compareTo(paramObject) == 0;
     }

     public int hashCode()
     {
         if (this.buffer == null)
             return 0;
         int i = 1;
         for (int j = 0; j < this.count; j++)
             i = 31 * i + this.buffer[j];
         return i;
     }

     public string toString()
     {
         return "octets.size=" + this.count;
     }

     public byte[] getBytes()
     {
         byte[] arrayOfByte = new byte[this.count];
         Array.Copy(this.buffer, 0, arrayOfByte, 0, this.count);
         return arrayOfByte;
     }

     public byte[] array()
     {
         return this.buffer;
     }

     public byte getByte(int paramInt)
     {
         return this.buffer[paramInt];
     }

     public void setByte(int paramInt, byte paramByte)
     {
         this.buffer[paramInt] = paramByte;
     }

     public void setString(string paramString)
     {
         //System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding(DEFAULT_CHARSET);
         this.buffer = Encoding.UTF8.GetBytes(paramString);
         this.count = this.buffer.Length;
     }

     public void setString(string paramString1, string paramString2)
     {
        this.buffer = Encoding.Unicode.GetBytes(paramString1);
        this.count = this.buffer.Length;
     }

      public string getString()
      {
          return Encoding.Unicode.GetString(this.buffer);
      }

      public string getString(string paramString )
      {
          return Encoding.UTF8.GetString(this.buffer);
      }
}
