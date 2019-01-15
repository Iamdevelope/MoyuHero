
using System;
using System.Text;
using System.Runtime.Serialization;

public class OctetsStream : Octets
{
	private static int MAXSPARE = 16384;
    private int pos = 0;
    private int tranpos = 0;

    public OctetsStream()
    {

    }

    public OctetsStream(int paramInt)
        : base(paramInt)
    {
      
    }

    public OctetsStream(Octets paramOctets)
        : base(paramOctets)
    {
        
    }

    public static OctetsStream wrap(Octets paramOctets)
    {
        OctetsStream localOctetsStream = new OctetsStream();
        localOctetsStream.swap(paramOctets);
        return localOctetsStream;
    }

    public object clone()
    {
        OctetsStream localOctetsStream = (OctetsStream)base.clone();
        localOctetsStream.pos = this.pos;
        localOctetsStream.tranpos = this.pos;
        return localOctetsStream;
    }

     public  bool eos()
     {
        return this.pos == size();
     }

      public int position(int paramInt)
      {
        this.pos = paramInt;
        return this.pos;

      }

      public  int position()
      {
        return this.pos;
      }

      public  int remain()
      {
        return size() - this.pos;
      }

      public OctetsStream marshal( byte paramByte )
      {
          push_back(paramByte);
          return this;
      }

      public OctetsStream marshal( bool paramBoolean )
      {
          push_back((byte)(paramBoolean ? 1 : 0));
          return this;
      }

      public OctetsStream marshal(short paramShort)
      {
          return marshal((byte)(paramShort >> 8)).marshal((byte)paramShort);
      }

      public OctetsStream marshal(char paramChar)
      {
          return marshal((byte)(paramChar >> '\b')).marshal((byte)paramChar);
      }

      public OctetsStream marshal(int paramInt)
      {
          return marshal((byte)(paramInt >> 24)).marshal((byte)(paramInt >> 16)).marshal((byte)(paramInt >> 8)).marshal((byte)paramInt);
      }

      public OctetsStream marshal(long paramLong)
      {
          return marshal((byte)(int)(paramLong >> 56)).marshal((byte)(int)(paramLong >> 48)).marshal((byte)(int)(paramLong >> 40)).marshal((byte)(int)(paramLong >> 32)).marshal((byte)(int)(paramLong >> 24)).marshal((byte)(int)(paramLong >> 16)).marshal((byte)(int)(paramLong >> 8)).marshal((byte)(int)paramLong);
      }

      public OctetsStream marshal(byte[] paramArrayOfByte)
      {
          compact_uint32(paramArrayOfByte.Length);
          insert(size(), paramArrayOfByte);
          return this;
      }

      public OctetsStream marshal(float paramFloat)
      {
          byte[] bytes = BitConverter.GetBytes(paramFloat);
          return marshal(bytes);
      }

      public OctetsStream marshal(double paramDouble)
      {
          return marshal(BitConverter.DoubleToInt64Bits(paramDouble));
      }

      public OctetsStream compact_uint32(int paramInt)
      {
          if (paramInt < 64)
              return marshal((byte)paramInt);
          if (paramInt < 16384)
              return marshal((short)(paramInt | 0x8000));
          if (paramInt < 536870912)
              return marshal((int)(paramInt | 0xC0000000));

          marshal((byte)32);
          return marshal(paramInt);
      }

      public OctetsStream compact_sint32(int paramInt)
      {
          if (paramInt >= 0)
          {
              if (paramInt < 64)
                  return marshal((byte)paramInt);
              if (paramInt < 8192)
                  return marshal((short)(paramInt | 0x8000));
              if (paramInt < 268435456)
                  return marshal((int)(paramInt | 0xC0000000));

              marshal((byte)32);
              return marshal(paramInt);
          }
          if (-paramInt > 0)
          {
              paramInt = -paramInt;
              if (paramInt < 64)
                  return marshal((byte)(paramInt | 0x40));
              if (paramInt < 8192)
                  return marshal((short)(paramInt | 0xA000));
              if (paramInt < 268435456)
                  return marshal(paramInt | 0xD0000000);

              marshal((byte)16);
              return marshal(paramInt);
          }

          marshal((byte)16);
          return marshal(paramInt);
      }

      public OctetsStream marshal(GNET.Marshal paramMarshal)
      {
          return paramMarshal.marshal(this);
      }

      public OctetsStream marshal(Octets paramOctets)
      {
          compact_uint32(paramOctets.size());
          insert(size(),paramOctets);
          return this;
      }

      public OctetsStream marshal(string paramString)
      {
          return marshal(paramString,"UTF-16LE");
      }

      public OctetsStream marshal(string paramString1, string paramString2)
      {
          try
          {
              byte[] _buff = Encoding.Unicode.GetBytes(paramString1);
              marshal(_buff);
              
          }
          catch ( System.Exception ex )
          {
              throw new SerializationException(ex.Message);
          }
          return this;
      }

      public OctetsStream Begin()
      {
          this.tranpos = this.pos;
          return this;
      }

      public OctetsStream Rollback()
      {
          this.pos = this.tranpos;
          return this;
      }

      public OctetsStream Commit()
      {
          if (this.pos >= 16384)
          {
              erase(0, this.pos);
              this.pos = 0;
          }
          return this;
      }
       
      public byte unmarshal_byte()
      {
        if (this.pos + 1 > size())
          throw new MarshalException();
        return getByte(this.pos++);
      }

      public bool unmarshal_boolean()
      {
         return unmarshal_byte() == 1;
      }

      public short unmarshal_short()
      {
        if (this.pos + 2 > size())
          throw new MarshalException();

        int i = getByte(this.pos++);
        int j = getByte(this.pos++);
        return (short)(i << 8 | j & 0xFF);
      }

      public char unmarshal_char()
      {
        if (this.pos + 2 > size())
          throw new MarshalException();
        int i = getByte(this.pos++);
        int j = getByte(this.pos++);
        return (char)(i << 8 | j & 0xFF);
      }

      public int unmarshal_int()
      {
        if (this.pos + 4 > size())
           throw new MarshalException();
       
        int i = getByte(this.pos++);
        int j = getByte(this.pos++);
        int k = getByte(this.pos++);
        int m = getByte(this.pos++);
        return (i & 0xFF) << 24 | (j & 0xFF) << 16 | (k & 0xFF) << 8 | (m & 0xFF) << 0;
      }

      public long unmarshal_long()
      {
        if (this.pos + 8 > size())
          throw new MarshalException();

        long i  = getByte(this.pos++);
        long j = getByte(this.pos++);
        long k = getByte(this.pos++);
        long m = getByte(this.pos++);
        long n = getByte(this.pos++);
        long i1 = getByte(this.pos++);
        long i2 = getByte(this.pos++);
        long i3 = getByte(this.pos++);

        return (i & 0xFF) << 56 | (j & 0xFF) << 48 | (k & 0xFF) << 40 | (m & 0xFF) << 32 | (n & 0xFF) << 24 | (i1 & 0xFF) << 16 | (i2 & 0xFF) << 8 | (i3 & 0xFF) << 0;
      }

      public float unmarshal_float()
      {
         byte[] bytes = BitConverter.GetBytes(unmarshal_int());
         float param  = BitConverter.ToSingle(bytes, 0);
         return param;
      }

      public double unmarshal_double()
      {
          return BitConverter.Int64BitsToDouble(unmarshal_long());
      }

      public int uncompact_uint32()
      {
        if (this.pos == size())
          throw new MarshalException();

        switch (getByte(this.pos) & 0xE0)
        {
        case 224:
          unmarshal_byte();
          return unmarshal_int();
        case 192:
          return unmarshal_int() & 0x3FFFFFFF;
        case 128:
        case 160:
          return unmarshal_short() & 0x7FFF;
        }
        return unmarshal_byte();
      }

      public int uncompact_sint32()
      {
        if (this.pos == size())
          throw new MarshalException();

        switch (getByte(this.pos) & 0xF0)
        {
        case 240:
          unmarshal_byte();
          return -unmarshal_int();
        case 224:
          unmarshal_byte();
          return unmarshal_int();
        case 208:
          return -(unmarshal_int() & 0x2FFFFFFF);
        case 192:
          return unmarshal_int() & 0x3FFFFFFF;
        case 160:
        case 176:
          return -(unmarshal_short() & 0x5FFF);
        case 128:
        case 144:
          return unmarshal_short() & 0x7FFF;
        case 64:
        case 80:
        case 96:
        case 112:
          return -(int)(unmarshal_byte() & 0xFFFFFFBF);
        }
        return unmarshal_byte();
      }

      public Octets unmarshal_Octets()
      {
        int i = uncompact_uint32();
        if (this.pos + i > size())
          throw new MarshalException();
        Octets localOctets = new Octets(this, this.pos, i);
        this.pos += i;
        return localOctets;
      }

      public byte[] unmarshal_bytes()
      {
        int i = uncompact_uint32();
        if (this.pos + i > size())
          throw new MarshalException();
        byte[] arrayOfByte = new byte[i];
        Array.Copy(array(), this.pos, arrayOfByte, 0, i);
        this.pos += i;
        return arrayOfByte;
      }


      public OctetsStream unmarshal( Octets paramOctets)
      {
        int i = uncompact_uint32();
        if (this.pos + i > size())
          throw new MarshalException();

        paramOctets.replace(this, this.pos, i);
        this.pos += i;
        return this;
      }

      public string unmarshal_String()
      {
          try
          {
              int i = uncompact_uint32();

              if (this.pos + i > size())
                  throw new MarshalException();


              int j = this.pos;
              this.pos += i;

              string s = System.Text.Encoding.Unicode.GetString(getBytes(), pos - i, i);
              if (s != null)
              {
                  s = s.Trim(new char[] { '\0' });
              }

              return s;
          }

          catch (System.Exception ex)
          {

          }
          throw new MarshalException();
      }

      public OctetsStream unmarshal(GNET.Marshal paramMarshal)
      {
            return paramMarshal.unmarshal(this);
      }


}
