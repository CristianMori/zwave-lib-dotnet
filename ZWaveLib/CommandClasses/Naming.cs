/*
    This file is an addition to the ZWaveLib Project source code.

    ZWaveLib is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    ZWaveLib is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with ZWaveLib.  If not, see <http://www.gnu.org/licenses/>.  
*/

/*
 *     Author: Cristian Mori <cristian.mori@gmail.com>
 *     Project Homepage: https://github.com/genielabs/zwave-lib-dotnet
 */

using System;
using System.Text;

//Add a command class to handle query of name
namespace ZWaveLib.CommandClasses
{
    public class Naming : ICommandClass
    {
        public CommandClass GetClassId()
        {
            return CommandClass.NodeNaming;
        }

        public NodeEvent GetEvent(ZWaveNode node, byte[] message)
        {
            NodeEvent nodeEvent = null;
            byte cmdType = message[1];
            if (cmdType == (byte)Command.BasicReport || cmdType == (byte)Command.BasicSet)
            {
                StringBuilder sb=new StringBuilder();
                for (Int32 ii = 3; ii < message.Length; ii++)
                    sb.Append((char)message[ii]);
                nodeEvent = new NodeEvent(node, EventParameter.Naming, (String)sb.ToString(), 0);
            }
            if (cmdType == (byte)Command.LoocationReply )
            {
                StringBuilder sb=new StringBuilder();
                for (Int32 ii = 3; ii < message.Length; ii++)
                    sb.Append((char)message[ii]);
                nodeEvent = new NodeEvent(node, EventParameter.Location, (String)sb.ToString(), 0);
            }
            return nodeEvent;
        }

        public static ZWaveMessage Set(ZWaveNode node, int value)
        {
            return node.SendDataRequest(new byte[] { 
                (byte)CommandClass.Basic, 
                (byte)Command.BasicSet, 
                byte.Parse(value.ToString())
            });
        }

        public static ZWaveMessage Get(ZWaveNode node)
        {
            return node.SendDataRequest(new byte[] { 
                (byte)CommandClass.NodeNaming,
                (byte)Command.BasicGet
            });
        }

        public static ZWaveMessage GetLocation(ZWaveNode node)
        {
            return node.SendDataRequest(new byte[] { 
                (byte)CommandClass.NodeNaming,
                (byte)Command.LocationGet
            });
        }

    }
}

