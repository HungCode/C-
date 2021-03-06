using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
public partial class _Default : System.Web.UI.Page
{
    protected int M;
    protected ArrayList G;          // luu phu thuoc ham = G[2i]-> G[2i+1]
    protected ArrayList F;          // luu phu thuoc ham cua G sau khi tach ve phai

    protected string G_content;
    protected string F_process_content;

    protected void Page_Load(object sender, EventArgs e)
    {
        F_process_content = "";
        G = new ArrayList();
        F = new ArrayList();

        button_Click();
    }

    protected void button_Click()
    {
        try
        {
            int d = 0;
            int value;
            Int32.TryParse(TextBox1.Text, out value);
            if (value > 2 && value < 11)
            {
                init_G(value);
                Label2.Text = G_content;
                F_process();
                //gan vao label de hien thi
                Label2.Text = G_content + "<br/>" + F_process_content;

            }
            else value = 5 / d;// -> day vao exception
        }
        catch (DivideByZeroException e1)
        {
            if (!IsPostBack)
            {
                Label2.Text = "Hãy nhập số nguyên dương >=3 và <=10";
            }
            else
                Label2.Text = "Dữ liệu đầu vào không hợp lệ, số nhập vào là số nguyên >=3 và <=10";
        }
    }

    protected void init_G(int m)
    {
        M = m;
        Random rd = new Random();

        int flag_tmp = 0;
        G_content += "Khởi tạo G = {";
        for (int j = 0; j < M; j++)
        {
            //tao 1 phu thuoc ham e->f
            string e = "", f = "";
            int e_ind = rd.Next(1, M);
            int f_ind = rd.Next(1, M);
            int sum_ef = e_ind + f_ind;

            //tao 1 mang cac ki tu khac nhau do dai = do dai (e + f)//
            ArrayList ef = new ArrayList();
            for (int i = 0; i < sum_ef; i++)
            {
                char x;
                do
                {
                    x = (char)rd.Next(65, 90);//90
                }
                while (ef.Contains(x));
                ef.Add(x);
            }
            //-----------------------------------------------------//

            // tao e, f roi them vao G
            for (int i = 0; i < e_ind; i++)
                e = e + ef[i];
            for (int i = e_ind; i < sum_ef; i++)
                f = f + ef[i];

            G.Add(e);
            G.Add(f);
            if (flag_tmp == 0) G_content += e + "->" + f;
            else G_content += " , " + e + "->" + f;
            flag_tmp = 1;
        }
        G_content += "}";

    }

    protected void F_process()
    {
        //------- tach G thanh cac phu thuoc ham ma ve phai chi co 1 thuoc tinh vao F 
        for (int i = 0; i < M; i++)
        {
            // ---- F chan la ve trai, le la ve phai F0 -> F1, F2-> F3 ...
            char[] f_ToCharArray = G[i + 1].ToString().ToCharArray();
            for (int j = 0; j < f_ToCharArray.Length; j++)
            {
                F.Add(G[i]);
                F.Add(f_ToCharArray[j]);
            }
        }
        F_process_content = F_process_content + "&emsp;*** Biến đồi G thành: F = {";
        for (int i = 0; i < F.Count; i = i + 2)
        {
            if (i != (F.Count - 2)) F_process_content = F_process_content + F[i] + "->" + F[i + 1] + ",";
            else F_process_content = F_process_content + F[i] + "->" + F[i + 1];
        }
        F_process_content = F_process_content + "}";
        //---

        //------- Kiem tra cac phu thuoc ham du thua
        int buoc = 0;
        for (int i = 0; i < F.Count; i = i + 2)
        {
            //kiem tra F[i]->F[i+1] du thua
            // &emsp; tao khoang trong = 4 dau cach trong html
            F_process_content = F_process_content + "<br/>&emsp;&emsp;+ Bước " + (++buoc) + ": "
                + "Kiểm tra " + F[i].ToString() + "->" + F[i + 1].ToString();
            ArrayList e_arr = new ArrayList(F[i].ToString().ToCharArray());
            for (int j = 0; j < e_arr.Count; j++)
            {
                // tinh e_arr[j] +
                ArrayList ej_extend = new ArrayList();
                ej_extend.Add(e_arr[j]);

                bool change = false;
                do
                {
                    change = false;
                    for (int k = 0; k < F.Count; k = k + 2)
                    {
                        if (k != i)
                        {
                            bool check = true;
                            ArrayList Fk_arr = new ArrayList(F[k].ToString().ToCharArray());
                            for (int l = 0; l < Fk_arr.Count; l++)
                            {
                                if (!ej_extend.Contains(Fk_arr[l]))
                                {
                                    check = false;
                                    break;
                                }
                            }
                            if (check && !ej_extend.Contains(F[k + 1]))
                            {
                                ej_extend.Add(F[k + 1]);
                                change = true;
                            }
                        }
                    }
                } while (change == true);

                //kiem tra e_arr[j]+ co chua F[i+1] ko, neu chua thi gan = "-"
                if (ej_extend.Contains(F[i + 1])) e_arr[j] = "";
            }
            string F_before = F[i].ToString();
            F[i] = "";
            for (int j = 0; j < e_arr.Count; j++)
            {
                F[i] = F[i] + e_arr[j].ToString();
            }

            if (F[i] == "")
            {
                F_process_content = F_process_content + ". ---  dư thừa  ---";
                F.Remove(F[i + 1]);
                F.Remove(F[i]);
            }
            else F_process_content = F_process_content + ". Kết quả vế trái:  " + F[i];
            //-----------
        }

        F = gather_F(F);

        //---------Ket qua
        F_process_content = F_process_content + "<br/>Kết quả: F = {";
        for (int i = 0; i < F.Count; i = i + 2)
        {
            if (F[i] != "")
            {
                if (i != (F.Count - 2)) F_process_content = F_process_content + F[i] + "->" + F[i + 1] + ",";
                else F_process_content = F_process_content + F[i] + "->" + F[i + 1];
            }
        }
        F_process_content = F_process_content + "}";
        //--------
    }

    protected ArrayList gather_F(ArrayList F)
    {
        // gop ve phai
        ArrayList F_result = new ArrayList();
        ArrayList F_vp = new ArrayList();
        ArrayList F_check = new ArrayList();//kiem tra cac phan tu F da xet chua
        for (int i = 0; i < F.Count; i = i + 2)
        {
            if (!F_check.Contains(i))
            {
                ArrayList F_tmp = new ArrayList();
                F_tmp.Add(F[i + 1]);
                F_check.Add(i);
                for (int j = i + 2; j < F.Count; j = j + 2)
                {
                    if (F[i].ToString() == F[j].ToString() && !F_tmp.Contains(F[j + 1].ToString()))
                    {
                        F_tmp.Add(F[j + 1].ToString());
                        F_check.Add(j);
                    }
                }
                string x = "";
                for (int j = 0; j < F_tmp.Count; j++)
                {
                    x = x + F_tmp[j].ToString();
                }
                F_vp.Add(F[i]);
                F_vp.Add(x);
            }
        }

        //gop ve trai
        F_check = new ArrayList();//kiem tra cac phan tu F da xet chua
        for (int i = 1; i < F_vp.Count; i = i + 2)
        {
            if (!F_check.Contains(i))
            {
                ArrayList F_tmp = new ArrayList();
                F_tmp.Add(F_vp[i - 1]);
                F_check.Add(i);
                for (int j = i + 2; j < F_vp.Count; j = j + 2)
                {
                    if (F_vp[i].ToString() == F_vp[j].ToString() && !F_tmp.Contains(F_vp[j - 1].ToString()))
                    {
                        F_tmp.Add(F_vp[j - 1].ToString());
                        F_check.Add(j);
                    }
                }
                string x = "";
                for (int j = 0; j < F_tmp.Count; j++)
                {
                    x = x + F_tmp[j].ToString();
                }
                F_result.Add(x);
                F_result.Add(F_vp[i]);
            }
        }
        return F_result;
    }
}
