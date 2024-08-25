using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace OBJ_to_MainAssembly
{
    public struct Point
    {
        public float x, y, z;
    };
    public struct Face
    {
        public int p1, p2, p3, p4;
    };
    public struct Segment
    {
        public int p1, p2;
    };
    internal class Converter
    {
        BackgroundWorker worker;
        public Converter(BackgroundWorker worker) {
            this.worker = worker;
        }
        public void Convert(Point[] points, Face[] faces, float creationScale = 1, bool InvertZY = false, bool RoundVertexPos = false, bool Mirror = false, char CreationForward = 'X', bool ShowFrameSegments = false)
        {
            worker.ReportProgress(0);

            HashSet<Segment> segments;
            List<int> NotMirroredPoints;

            NotMirroredPoints = new List<int>();
            segments = new HashSet<Segment>();
            Point[] newPoints = new Point[points.Length];
            Face[] newFaces = new Face[faces.Length];
            int goodNewFaces = 0;
            for (int vert = 0; vert < newPoints.Length; vert++)
            {
                switch (CreationForward)
                {
                    case '\0':
                    case 'X':
                        newPoints[vert].x = points[vert].x;
                        newPoints[vert].y = InvertZY ? points[vert].z : points[vert].y;
                        newPoints[vert].z = InvertZY ? points[vert].y : points[vert].z;
                        break;
                    case 'Y':
                        newPoints[vert].x = points[vert].y;
                        newPoints[vert].y = InvertZY ? points[vert].z : points[vert].x;
                        newPoints[vert].z = InvertZY ? points[vert].x : points[vert].z;
                        break;
                    case 'Z':
                        newPoints[vert].x = points[vert].z;
                        newPoints[vert].y = InvertZY ? points[vert].x : points[vert].y;
                        newPoints[vert].z = InvertZY ? points[vert].y : points[vert].x;
                        break;
                }
                if (RoundVertexPos)
                {
                    newPoints[vert].x = (float)(Math.Round(newPoints[vert].x * creationScale * 0.8f) / 0.8f);
                    newPoints[vert].y = (float)(Math.Round(newPoints[vert].y * creationScale * 0.8f) / 0.8f);
                    newPoints[vert].z = (float)(Math.Round(newPoints[vert].z * creationScale * 0.8f) / 0.8f);
                }
                else
                {
                    newPoints[vert].x *= creationScale;
                    newPoints[vert].y *= creationScale;
                    newPoints[vert].z *= creationScale;
                }
                if (Mirror && newPoints[vert].y < 0)
                {
                    NotMirroredPoints.Add(vert + 1);
                    newPoints[vert].y = 0;
                }

                worker.ReportProgress(40 * vert / newPoints.Length);
            }

            for (int face = 0; face < faces.Length; face++)
            {
                if (faces[face].p4 == 0)
                {
                    if (VertexNotInList(faces[face].p1, NotMirroredPoints) || VertexNotInList(faces[face].p2, NotMirroredPoints) || VertexNotInList(faces[face].p3, NotMirroredPoints))
                    {
                        Segment segmentInfo = new Segment();

                        segmentInfo.p1 = faces[face].p1;
                        segmentInfo.p2 = faces[face].p2;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        segmentInfo.p1 = faces[face].p2;
                        segmentInfo.p2 = faces[face].p3;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        segmentInfo.p1 = faces[face].p3;
                        segmentInfo.p2 = faces[face].p1;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        newFaces[goodNewFaces] = faces[face];
                        goodNewFaces++;
                    }
                }
                else
                { //4 VERTS
                    if (VertexNotInList(faces[face].p1, NotMirroredPoints) || VertexNotInList(faces[face].p2, NotMirroredPoints) || VertexNotInList(faces[face].p3, NotMirroredPoints) || VertexNotInList(faces[face].p4, NotMirroredPoints))
                    {
                        Segment segmentInfo = new Segment();

                        segmentInfo.p1 = faces[face].p1;
                        segmentInfo.p2 = faces[face].p2;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        segmentInfo.p1 = faces[face].p2;
                        segmentInfo.p2 = faces[face].p3;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        segmentInfo.p1 = faces[face].p3;
                        segmentInfo.p2 = faces[face].p4;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        segmentInfo.p1 = faces[face].p4;
                        segmentInfo.p2 = faces[face].p1;
                        if (SegmentNotInList(segmentInfo, segments)) segments.Add(segmentInfo);

                        newFaces[goodNewFaces] = faces[face];
                        goodNewFaces++;
                    }
                }

                worker.ReportProgress(40 * face / faces.Length + 40);
            }


            FileStream BaseJSON;
            string currentDirectory = Directory.GetCurrentDirectory();

            //FinalCode[46] = '\t\t\t"mirror": "MirrorY",'
            try
            {
                BaseJSON = File.OpenRead(currentDirectory + "\\BaseCode.json");
            }
            catch (IOException ex)
            {
                MessageBox.Show("An IO exception occurred while attempting to open the file: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to open the file: " + ex.Message);
                return;
            }
            const int pointsWritingLine = 41;
            int segmentsWritingLine = 43 + points.Length;
            int frameWritingLine = 2 + segmentsWritingLine + segments.Count;

            worker.ReportProgress(55);

            int i = 0;
            int x;
            string line;

            FileStream OutputJSON;
            try
            {
                File.WriteAllText(currentDirectory + "\\Output\\Output.schematic.json", "");
                OutputJSON = File.OpenWrite(currentDirectory + "\\Output\\Output.schematic.json");
            }
            catch (IOException ex)
            {
                MessageBox.Show("An IO exception occurred while attempting to open the file: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to open the file: " + ex.Message);
                return;
            }

            using (StreamWriter writer = new StreamWriter(OutputJSON))
            {
                using (StreamReader reader = new StreamReader(BaseJSON))
                {
                    writer.Write("");

                    while ((line = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(line);
                        i++;
                        if (i == pointsWritingLine)
                        {
                            for (x = 0; x < points.Length; x++)
                            {
                                if (x != points.Length - 1)
                                    writer.WriteLine(string.Format("\t\t\t\t\"{0}\": {{ \"x\": {1}, \"y\": {2}, \"z\": {3} }},", x + 1, newPoints[x].x.ToString(), newPoints[x].y.ToString(), newPoints[x].z.ToString()));
                                else
                                    writer.WriteLine(string.Format("\t\t\t\t\"{0}\": {{ \"x\": {1}, \"y\": {2}, \"z\": {3} }}", x + 1, newPoints[x].x.ToString(), newPoints[x].y.ToString(), newPoints[x].z.ToString()));
                                i++;
                            }
                        }
                        else if (i == segmentsWritingLine)
                        {
                            x = 0;
                            foreach (Segment seg in segments)
                            {
                                if (x != segments.Count - 1)
                                    writer.WriteLine(string.Format("\t\t\t\t\"{0}\": {{\n\t\t\t\t\t\"first\": {{ \"elemId\": {1} }},\n\t\t\t\t\t\"second\": {{ \"elemId\": {2} }},\n\t\t\t\t\t\"visible\": {3},\n\t\t\t\t\t\"properties\": {{\n\t\t\t\t\t\t\"values\": {{\n\t\t\t\t\t\t\t\"tint\": {{ \"tag\": \"EProperty_Tint\", \"integer\": 2 }},\n\t\t\t\t\t\t\t\"material\": {{ \"tag\": \"EProperty_Int\", \"integer\": 1 }}\n\t\t\t\t\t\t}}\n\t\t\t\t\t}}\n\t\t\t\t}},", x + 1, seg.p1, seg.p2, ShowFrameSegments ? "true" : "false"));
                                else
                                    writer.WriteLine(string.Format("\t\t\t\t\"{0}\": {{\n\t\t\t\t\t\"first\": {{ \"elemId\": {1} }},\n\t\t\t\t\t\"second\": {{ \"elemId\": {2} }},\n\t\t\t\t\t\"visible\": {3},\n\t\t\t\t\t\"properties\": {{\n\t\t\t\t\t\t\"values\": {{\n\t\t\t\t\t\t\t\"tint\": {{ \"tag\": \"EProperty_Tint\", \"integer\": 2 }},\n\t\t\t\t\t\t\t\"material\": {{ \"tag\": \"EProperty_Int\", \"integer\": 1 }}\n\t\t\t\t\t\t}}\n\t\t\t\t\t}}\n\t\t\t\t}}", x + 1, seg.p1, seg.p2, ShowFrameSegments ? "true" : "false"));
                                i++;
                                x++;
                            }
                        }
                        else if (i == frameWritingLine)
                        {
                            for (x = 0; x < goodNewFaces; x++)
                            {
                                if (x != goodNewFaces - 1)
                                    writer.WriteLine(string.Format("\t\t\t\t\"{0}\": {{\n\t\t\t\t\t\"points\": [\n\t\t\t\t\t\t{{ \"elemId\": {1} }},\n\t\t\t\t\t\t{{ \"elemId\": {2} }},\n\t\t\t\t\t\t{{ \"elemId\": {3} }},\n\t\t\t\t\t\t{{ \"elemId\": {4} }}\n\t\t\t\t\t],\n\t\t\t\t\t\"visible\": true,\n\t\t\t\t\t\"properties\": {{\n\t\t\t\t\t\t\"values\": {{\n\t\t\t\t\t\t\t\"tint\": {{ \"tag\": \"EProperty_Tint\", \"integer\": 0 }},\n\t\t\t\t\t\t\t\"material\": {{ \"tag\": \"EProperty_Int\", \"integer\": 0 }}\n\t\t\t\t\t\t}}\n\t\t\t\t\t}}\n\t\t\t\t}},", x + 1, newFaces[x].p1, newFaces[x].p2, newFaces[x].p3, newFaces[x].p4));
                                else
                                    writer.WriteLine(string.Format("\t\t\t\t\"{0}\": {{\n\t\t\t\t\t\"points\": [\n\t\t\t\t\t\t{{ \"elemId\": {1} }},\n\t\t\t\t\t\t{{ \"elemId\": {2} }},\n\t\t\t\t\t\t{{ \"elemId\": {3} }},\n\t\t\t\t\t\t{{ \"elemId\": {4} }}\n\t\t\t\t\t],\n\t\t\t\t\t\"visible\": true,\n\t\t\t\t\t\"properties\": {{\n\t\t\t\t\t\t\"values\": {{\n\t\t\t\t\t\t\t\"tint\": {{ \"tag\": \"EProperty_Tint\", \"integer\": 0 }},\n\t\t\t\t\t\t\t\"material\": {{ \"tag\": \"EProperty_Int\", \"integer\": 0 }}\n\t\t\t\t\t\t}}\n\t\t\t\t\t}}\n\t\t\t\t}}", x + 1, newFaces[x].p1, newFaces[x].p2, newFaces[x].p3, newFaces[x].p4));
                                i++;
                            }
                        }

                        worker.ReportProgress(80 + 15 * i / (goodNewFaces + frameWritingLine));
                    }
                }

            }
            BaseJSON.Close();
            OutputJSON.Close();

            worker.ReportProgress(97);

            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RoboBuild", "Saved", "bots", ".bot.Output");
            string copyPath = Path.Combine(currentDirectory, "Output");

            // Delete any existing files in the output directory
            if (Directory.Exists(dirPath))
            {
                foreach (string file in Directory.GetFiles(dirPath))
                {
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(dirPath);
            }
            // Copy the output files to the destination directory
            foreach (string file in Directory.GetFiles(copyPath))
            {
                File.Copy(file, Path.Combine(dirPath, Path.GetFileName(file)), true);
            }
            worker.ReportProgress(100);
        }

        public bool convertSTL(string filePath, out Point[] _points, out Face[] _faces)
        {
            _points = new Point[0];
            _faces = new Face[0];
            FileStream ImportedSTL;
            try
            {
                ImportedSTL = File.OpenRead(filePath);
            }
            catch (IOException ex)
            {
                MessageBox.Show("An IO exception occurred while attempting to open the file: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to open the file: " + ex.Message);
                return false;
            }

            List<Point> points = new List<Point>();
            Dictionary<Point, int> pointIndices = new Dictionary<Point, int>();

            using (BinaryReader reader = new BinaryReader(ImportedSTL))
            {
                worker.ReportProgress(0);
                // Skip the header
                reader.ReadBytes(80);

                // Read the number of triangles
                uint numTriangles = reader.ReadUInt32();
                Console.WriteLine("Triangles : " + numTriangles.ToString());

                if (numTriangles > 6000)
                {
                    MessageBox.Show("Model contains more than the recommended 6000 max faces, good luck");
                }

                _faces = new Face[numTriangles];

                // Read each triangle
                for (int i = 0; i < numTriangles; i++)
                {
                    // Read the normal vector (ignored)
                    reader.ReadBytes(12);

                    // Read the three vertices of the triangle
                    int[] vertexIndices = new int[3];
                    for (int j = 0; j < 3; j++)
                    {

                        Point vertex = new Point { x = reader.ReadSingle(), y = reader.ReadSingle(), z = reader.ReadSingle() };

                        if (!pointIndices.TryGetValue(vertex, out int index))
                        {
                            index = points.Count;
                            points.Add(vertex);
                            pointIndices.Add(vertex, index);
                        }

                        vertexIndices[j] = index + 1;
                        /*
                        Point vertex = new Point { x = reader.ReadSingle(), y = reader.ReadSingle(), z = reader.ReadSingle() };
                        int index = points.IndexOf(vertex);
                        if (index == -1)
                        {
                            index = points.Count;
                            points.Add(vertex);
                        }
                        vertexIndices[j] = index + 1;*/
                    }


                    // Add the face to the list
                    _faces[i] = new Face { p1 = vertexIndices[0], p2 = vertexIndices[1], p3 = vertexIndices[2], p4 = 0 };

                    // Skip the attribute byte count (ignored)
                    reader.ReadUInt16();

                    worker.ReportProgress((int)(100 * i / numTriangles));
                }
            }

            Console.WriteLine("Sucessfully imported 3d model");
            Console.WriteLine("{0} points, {1} faces", points.Count, _faces.Length);

            _points = points.ToArray();

            return true;
        }
        public bool convertOBJ(string filePath, out Point[] _points, out Face[] _faces)
        {
            _points = new Point[0];
            _faces = new Face[0];
            FileStream ImportedOBJ;

            try
            {
                ImportedOBJ = File.OpenRead(filePath);
            }
            catch (IOException ex)
            {
                MessageBox.Show("An IO exception occurred while attempting to open the file: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to open the file: " + ex.Message);
                return false;
            }

            int faceCount = 0;
            int parts = 0;

            string line;

            List<Point> points = new List<Point>();
            List<Face> faces = new List<Face>();
            float[] Pos = new float[3];

            worker.ReportProgress(5);

            using (StreamReader reader = new StreamReader(ImportedOBJ))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length <= 2) continue;
                    switch (line.Substring(0, 2))
                    {
                        case "v ":
                            string[] values = line.Split(' ');
                            float.TryParse(values[1], out Pos[0]);
                            float.TryParse(values[2], out Pos[1]);
                            float.TryParse(values[3], out Pos[2]);

                            points.Add(new Point { x = Pos[0], y = Pos[1], z = Pos[2] });

                            break;
                        case "f ":
                            string[] tokens = line.Split(' ');
                            int count = tokens.Length;
                            Face newFace = new Face();
                            if (count > 5)
                            {
                                MessageBox.Show("Model contains ngons with more than 4 verts");
                                return false;
                            }

                            int index = tokens[1].IndexOf('/');
                            if (index >= 0)
                                int.TryParse(tokens[1].Substring(0, index), out newFace.p1);
                            else
                                int.TryParse(tokens[1], out newFace.p1);
                            index = tokens[2].IndexOf('/');
                            if (index >= 0)
                                int.TryParse(tokens[2].Substring(0, index), out newFace.p2);
                            else
                                int.TryParse(tokens[2], out newFace.p2);
                            index = tokens[3].IndexOf('/');
                            if (index >= 0)
                                int.TryParse(tokens[3].Substring(0, index), out newFace.p3);
                            else
                                int.TryParse(tokens[3], out newFace.p3);

                            if (count == 5)
                            {
                                index = tokens[4].IndexOf('/');
                                if (index >= 0)
                                    int.TryParse(tokens[4].Substring(0, index), out newFace.p4);
                                else
                                    int.TryParse(tokens[4], out newFace.p4);
                            }
                            else
                                newFace.p4 = 0;

                            faces.Add(newFace);

                            faceCount++;
                            break;
                        case " o ":
                            parts++;
                            break;
                    }
                }
            }
            worker.ReportProgress(40);

            Console.WriteLine("Sucessfully imported 3d model");
            Console.WriteLine("{0} points, {1} faces", points.Count, faces.Count);

            _points = points.ToArray();
            _faces = faces.ToArray();

            return true;
        }
        static bool SegmentNotInList(Segment seg, HashSet<Segment> segList)
        {
            Segment invertedSeg = new Segment { p1 = seg.p2, p2 = seg.p1 };
            if (segList.Contains(seg) || segList.Contains(invertedSeg))
                return false;
            else
                return true;
        }
        static bool VertexNotInList(int vert, List<int> NotMirroredPoints)
        {
            return !NotMirroredPoints.Contains(vert);
        }
    }
}
