using ERPMaster.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.Security.Permissions;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;
using System.Security;

namespace Folder_Browser
{
    public static class Util
    {
        public static class Template
        {
            public static Dictionary<ArchcorpDirectoryGroups, string> GetArchcorpDirectoryGroups()
            {
                var directories = new Dictionary<ArchcorpDirectoryGroups, string>()
                {
                    { ArchcorpDirectoryGroups.Architects, "dat_aedxa_all Architects"},
                    { ArchcorpDirectoryGroups.Mechanical, "dat_aedxa_all Mechanical"},
                    { ArchcorpDirectoryGroups.Electrical, "dat_aedxa_all Electrical"},
                    { ArchcorpDirectoryGroups.Structures, "dat_aedxa_all Structures"},
                    { ArchcorpDirectoryGroups.Intr, "dat_aedxa_all Interiors"},
                    { ArchcorpDirectoryGroups.PMs, "dat_aedxa_all PMs"},
                    { ArchcorpDirectoryGroups.Admin, "dat_aedxa_all admins"},
                    { ArchcorpDirectoryGroups.Architectural_CAD, "dat_aedxa_all Architectural CAD"},
                    { ArchcorpDirectoryGroups.Mechanical_CAD, "dat_aedxa_all Mechanical CAD"},
                    { ArchcorpDirectoryGroups.Electrical_CAD, "dat_aedxa_all Electrical CAD"},
                    { ArchcorpDirectoryGroups.Dubai_Office_Staff, "dat_aedxa_all Dubai Office Staff"},
                    { ArchcorpDirectoryGroups.QS, "dat_aedxa_all QS"},
                    { ArchcorpDirectoryGroups.Structural_CAD, "dat_aedxa_all Structural CAD"},
                    { ArchcorpDirectoryGroups.Systems_Administrator, "dat_aedxa_all Systems Administrator"},

                };



                return directories;
            }

            public static Folder_Browser.Folder GetProjectTemplate_Folderstructure(string projectname)
            {

                var archcorpDirectories = GetArchcorpDirectoryGroups();

                #region Level 0
                var projectFolder = new Folder_Browser.Folder
                {
                    Name = projectname,
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                #endregion

                #region Level 1

                #region Level 1 - Project
                var docs = new Folder_Browser.Folder
                {
                    Name = "Docs",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                projectFolder.NestedFolders.Add(docs);

                var dwgs = new Folder_Browser.Folder
                {
                    Name = "Dwgs",
                };
                projectFolder.NestedFolders.Add(dwgs);

                var exchange = new Folder_Browser.Folder
                {
                    Name = "Exchange",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                projectFolder.NestedFolders.Add(exchange);

                #endregion

                #endregion

                #region Level 2

                #region Level 2 - Project/Docs/
                var correspondence = new Folder_Browser.Folder
                {
                    Name = "Correspondence",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Admin], archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                docs.NestedFolders.Add(correspondence);

                #region Project/Docs/Correspondence
                var c_in = new Folder_Browser.Folder
                {
                    Name = "IN",
                };
                correspondence.NestedFolders.Add(c_in);

                var c_out = new Folder_Browser.Folder
                {
                    Name = "OUT",
                };
                correspondence.NestedFolders.Add(c_out);

                #region Project/Docs/Correspondence/OUT
                var c_draft = new Folder_Browser.Folder
                {
                    Name = "Draft",
                };
                c_out.NestedFolders.Add(c_draft);

                var c_final = new Folder_Browser.Folder
                {
                    Name = "Final",
                };
                c_out.NestedFolders.Add(c_final);

                var c_receipt = new Folder_Browser.Folder
                {
                    Name = "Receipt",
                };
                c_out.NestedFolders.Add(c_receipt);
                #endregion


                #endregion

                var mom = new Folder_Browser.Folder
                {
                    Name = "MOM",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Architects], archcorpDirectories[ArchcorpDirectoryGroups.Mechanical], archcorpDirectories[ArchcorpDirectoryGroups.Electrical], archcorpDirectories[ArchcorpDirectoryGroups.Structures], archcorpDirectories[ArchcorpDirectoryGroups.Intr], archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.Admin] },
                };
                docs.NestedFolders.Add(mom);

                #region Project/Docs/MOM
                var m_authority = new Folder_Browser.Folder
                {
                    Name = "Authority",
                };
                mom.NestedFolders.Add(m_authority);

                var m_client = new Folder_Browser.Folder
                {
                    Name = "Client",
                };
                mom.NestedFolders.Add(m_client);

                var m_consultant = new Folder_Browser.Folder
                {
                    Name = "Consultant",
                };
                mom.NestedFolders.Add(m_consultant);

                var m_contractor = new Folder_Browser.Folder
                {
                    Name = "Contractor",
                };
                mom.NestedFolders.Add(m_contractor);

                var m_team = new Folder_Browser.Folder
                {
                    Name = "Team",
                };
                mom.NestedFolders.Add(m_team);
                #endregion


                var pm = new Folder_Browser.Folder
                {
                    Name = "PM",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                docs.NestedFolders.Add(pm);
                #region Project/Docs/PM
                var pm_correspondence = new Folder_Browser.Folder
                {
                    Name = "Correspondence",
                };
                pm.NestedFolders.Add(pm_correspondence);

                #region Project/Docs/PM/Correspondence
                var pm_in = new Folder_Browser.Folder
                {
                    Name = "IN",
                };
                pm_correspondence.NestedFolders.Add(pm_in);

                var pm_out = new Folder_Browser.Folder
                {
                    Name = "OUT",
                };
                pm_correspondence.NestedFolders.Add(pm_out);
                #endregion

                var pm_legal = new Folder_Browser.Folder
                {
                    Name = "Legal",
                };
                pm.NestedFolders.Add(pm_legal);

                #region Project/Docs/PM/Legal
                var pm_agreement = new Folder_Browser.Folder
                {
                    Name = "Agreement",
                };
                pm_legal.NestedFolders.Add(pm_agreement);

                var pm_insurance = new Folder_Browser.Folder
                {
                    Name = "Insurance",
                };
                pm_legal.NestedFolders.Add(pm_insurance);
                #endregion

                var pm_projectaccount = new Folder_Browser.Folder
                {
                    Name = "Project Account",
                };
                pm.NestedFolders.Add(pm_projectaccount);

                #region Project/Docs/PM/Legal/Project Account
                var pm_invoice = new Folder_Browser.Folder
                {
                    Name = "Invoices",
                };
                pm_projectaccount.NestedFolders.Add(pm_invoice);
                #endregion

                var pm_proposal = new Folder_Browser.Folder
                {
                    Name = "Proposals",
                };
                pm.NestedFolders.Add(pm_proposal);

                #region Project/Docs/PM/Legal/Project Account/Proposals

                var proposal_in = new Folder_Browser.Folder
                {
                    Name = "IN",
                };
                pm_proposal.NestedFolders.Add(proposal_in);

                var proposal_out = new Folder_Browser.Folder
                {
                    Name = "OUT",
                };
                pm_proposal.NestedFolders.Add(proposal_out);
                #endregion

                var pm_scope = new Folder_Browser.Folder
                {
                    Name = "Scope",
                };
                pm.NestedFolders.Add(pm_scope);

                var pm_variation = new Folder_Browser.Folder
                {
                    Name = "Variations",
                };
                pm.NestedFolders.Add(pm_variation);
                #endregion


                var qs = new Folder_Browser.Folder
                {
                    Name = "QS",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.QS] },
                };
                docs.NestedFolders.Add(qs);

                #region Project/Docs/QS
                var qs_boq = new Folder_Browser.Folder
                {
                    Name = "BOQ",
                };
                qs.NestedFolders.Add(qs_boq);

                var qs_quotecomparision = new Folder_Browser.Folder
                {
                    Name = "Quote Comparisions",
                };
                qs.NestedFolders.Add(qs_quotecomparision);

                var qs_tenderreport = new Folder_Browser.Folder
                {
                    Name = "Tender Report",
                };
                qs.NestedFolders.Add(qs_tenderreport);

                var qs_vol = new Folder_Browser.Folder
                {
                    Name = "VOL 1",
                };
                qs.NestedFolders.Add(qs_vol);
                #endregion


                var reports = new Folder_Browser.Folder
                {
                    Name = "Reports",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Architects], archcorpDirectories[ArchcorpDirectoryGroups.Mechanical], archcorpDirectories[ArchcorpDirectoryGroups.Electrical], archcorpDirectories[ArchcorpDirectoryGroups.Structures], archcorpDirectories[ArchcorpDirectoryGroups.Intr], archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.Admin] },
                };
                docs.NestedFolders.Add(reports);

                var technical = new Folder_Browser.Folder
                {
                    Name = "Technical",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                docs.NestedFolders.Add(technical);



                #endregion

                #region Level 2 - Project/DWGS/
                var archive = new Folder_Browser.Folder
                {
                    Name = "Archive",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                dwgs.NestedFolders.Add(archive);

                var latest = new Folder_Browser.Folder
                {
                    Name = "Latest",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                dwgs.NestedFolders.Add(latest);

                var ongoing = new Folder_Browser.Folder
                {
                    Name = "Ongoing",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                };
                dwgs.NestedFolders.Add(ongoing);

                #endregion

                #region Level 2 - Project/Exchange/

                var _in = new Folder_Browser.Folder
                {
                    Name = "IN",
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                exchange.NestedFolders.Add(_in);

                #region Project/Exchange/IN
                var _in_authority = new Folder_Browser.Folder
                {
                    Name = "Authorities",
                };
                _in.NestedFolders.Add(_in_authority);

                var _in_client = new Folder_Browser.Folder
                {
                    Name = "Client",
                };
                _in.NestedFolders.Add(_in_client);

                var _in_consultant = new Folder_Browser.Folder
                {
                    Name = "Consultant",
                };
                _in.NestedFolders.Add(_in_consultant);

                var _in_contractor = new Folder_Browser.Folder
                {
                    Name = "Contractor",
                };
                _in.NestedFolders.Add(_in_contractor);

                var _in_subconsultant = new Folder_Browser.Folder
                {
                    Name = "Sub-Consultant",
                };
                _in.NestedFolders.Add(_in_subconsultant);

                var _in_suppliers = new Folder_Browser.Folder
                {
                    Name = "Suppliers",
                };
                _in.NestedFolders.Add(_in_suppliers);

                var _in_team = new Folder_Browser.Folder
                {
                    Name = "Team",
                };
                _in.NestedFolders.Add(_in_team);

                #endregion


                var _out = new Folder_Browser.Folder
                {
                    Name = "OUT",
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                exchange.NestedFolders.Add(_out);

                #region Project/Exchange/OUT
                var _out_team = new Folder_Browser.Folder
                {
                    Name = "Team",
                };
                _out.NestedFolders.Add(_out_team);

                var _out_client = new Folder_Browser.Folder
                {
                    Name = "Client",
                };
                _out.NestedFolders.Add(_out_client);

                var _out_consultant = new Folder_Browser.Folder
                {
                    Name = "Consultant",
                };
                _out.NestedFolders.Add(_out_consultant);

                var _out_subconsultant = new Folder_Browser.Folder
                {
                    Name = "Sub-Consultant",
                };
                _out.NestedFolders.Add(_out_subconsultant);

                var _out_suppliers = new Folder_Browser.Folder
                {
                    Name = "Suppliers",
                };
                _out.NestedFolders.Add(_out_suppliers);

                var _out_ex_team = new Folder_Browser.Folder
                {
                    Name = "Team",
                };
                _out.NestedFolders.Add(_out_ex_team);

                #endregion

                #endregion

                #endregion

                #region Level 3

                #region Level 3 - Project/Docs/Technical
                var arch = new Folder_Browser.Folder
                {
                    Name = "Arch",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.Architects] },
                };
                technical.NestedFolders.Add(arch);


                #region Project/Docs/Technical/Arch
                var tech_arch_cal = new Folder_Browser.Folder
                {
                    Name = "Calcs",
                };
                arch.NestedFolders.Add(tech_arch_cal);

                var tech_arch_spec = new Folder_Browser.Folder
                {
                    Name = "Specs",
                };
                arch.NestedFolders.Add(tech_arch_spec);
                #endregion

                var intr = new Folder_Browser.Folder
                {
                    Name = "Intr",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.Intr] },
                };
                technical.NestedFolders.Add(intr);

                #region Project/Docs/Technical/Intr
                var tech_intr_cal = new Folder_Browser.Folder
                {
                    Name = "Calcs",
                };
                intr.NestedFolders.Add(tech_intr_cal);

                var tech_intr_spec = new Folder_Browser.Folder
                {
                    Name = "Specs",
                };
                intr.NestedFolders.Add(tech_intr_spec);
                #endregion

                var mep = new Folder_Browser.Folder
                {
                    Name = "MEP",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.Mechanical], archcorpDirectories[ArchcorpDirectoryGroups.Electrical] },
                };
                technical.NestedFolders.Add(mep);

                #region Project/Docs/Technical/MEP
                var tech_mep_cal = new Folder_Browser.Folder
                {
                    Name = "Calcs",
                };
                mep.NestedFolders.Add(tech_mep_cal);

                var tech_mep_spec = new Folder_Browser.Folder
                {
                    Name = "Specs",
                };
                mep.NestedFolders.Add(tech_mep_spec);
                #endregion

                var projectdocs = new Folder_Browser.Folder
                {
                    Name = "Project Docs",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                technical.NestedFolders.Add(projectdocs);

                var research = new Folder_Browser.Folder
                {
                    Name = "Research",
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Architects], archcorpDirectories[ArchcorpDirectoryGroups.Mechanical], archcorpDirectories[ArchcorpDirectoryGroups.Electrical], archcorpDirectories[ArchcorpDirectoryGroups.Structures], archcorpDirectories[ArchcorpDirectoryGroups.Intr], archcorpDirectories[ArchcorpDirectoryGroups.PMs], archcorpDirectories[ArchcorpDirectoryGroups.Admin] },
                };
                technical.NestedFolders.Add(research);

                #region Project/Docs/Technical/Research
                var research_arch = new Folder_Browser.Folder
                {
                    Name = "Arch",
                };
                research.NestedFolders.Add(research_arch);

                var research_intr = new Folder_Browser.Folder
                {
                    Name = "Intr",
                };
                research.NestedFolders.Add(research_intr);

                var research_mep = new Folder_Browser.Folder
                {
                    Name = "MEP",
                };
                research.NestedFolders.Add(research_mep);

                var research_str = new Folder_Browser.Folder
                {
                    Name = "Str",
                };
                research.NestedFolders.Add(research_str);

                #endregion

                var str = new Folder_Browser.Folder
                {
                    Name = "Str",
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Structures], archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                technical.NestedFolders.Add(str);

                #region Project/Docs/Technical/Str
                var str_verification = new Folder_Browser.Folder
                {
                    Name = "Verification",
                };
                str.NestedFolders.Add(str_verification);

                var str_calcs = new Folder_Browser.Folder
                {
                    Name = "Calcs",
                };
                str.NestedFolders.Add(str_calcs);

                var str_specs = new Folder_Browser.Folder
                {
                    Name = "Specs",
                };
                str.NestedFolders.Add(str_specs);

                #endregion
                #endregion

                #region Level 3 - Project/Dwg/Archive
                var a_arch = new Folder_Browser.Folder
                {
                    Name = "Arch",
                };
                archive.NestedFolders.Add(a_arch);

                var a_intr = new Folder_Browser.Folder
                {
                    Name = "Intr",
                };
                archive.NestedFolders.Add(a_intr);

                var a_mep = new Folder_Browser.Folder
                {
                    Name = "MEP",
                };
                archive.NestedFolders.Add(a_mep);

                var a_str = new Folder_Browser.Folder
                {
                    Name = "Str",
                };
                archive.NestedFolders.Add(a_str);

                var a_xref = new Folder_Browser.Folder
                {
                    Name = "Xref",
                };
                archive.NestedFolders.Add(a_xref);

                #endregion

                #region Level 3 - Project/Dwg/Latest
                var l_arch = new Folder_Browser.Folder
                {
                    Name = "Arch",
                };
                latest.NestedFolders.Add(l_arch);

                #region Project/DWG/Latest/Arch
                var l_arch_cd = new Folder_Browser.Folder
                {
                    Name = "CD",
                };
                l_arch.NestedFolders.Add(l_arch_cd);

                var l_arch_dd = new Folder_Browser.Folder
                {
                    Name = "DD",
                };
                l_arch.NestedFolders.Add(l_arch_dd);

                var l_arch_sd = new Folder_Browser.Folder
                {
                    Name = "SD",
                };
                l_arch.NestedFolders.Add(l_arch_sd);

                var l_arch_td = new Folder_Browser.Folder
                {
                    Name = "TD",
                };
                l_arch.NestedFolders.Add(l_arch_td);

                #endregion

                var l_intr = new Folder_Browser.Folder
                {
                    Name = "Intr",
                };
                latest.NestedFolders.Add(l_intr);

                var l_mep = new Folder_Browser.Folder
                {
                    Name = "MEP",
                };
                latest.NestedFolders.Add(l_mep);

                var l_str = new Folder_Browser.Folder
                {
                    Name = "Str",
                };
                latest.NestedFolders.Add(l_str);

                #region Project/DWG/Latest/Str
                var l_str_cd = new Folder_Browser.Folder
                {
                    Name = "CD",
                };
                l_str.NestedFolders.Add(l_str_cd);

                var l_str_dd = new Folder_Browser.Folder
                {
                    Name = "DD",
                };
                l_str.NestedFolders.Add(l_str_dd);

                var l_str_sd = new Folder_Browser.Folder
                {
                    Name = "SD",
                };
                l_str.NestedFolders.Add(l_str_sd);

                var l_str_td = new Folder_Browser.Folder
                {
                    Name = "TD",
                };
                l_str.NestedFolders.Add(l_str_td);
                #endregion

                var l_xref = new Folder_Browser.Folder
                {
                    Name = "Xref",
                };
                latest.NestedFolders.Add(l_xref);
                #endregion

                #region Level 3 - Project/Dwg/Ongoing

                var o_arch = new Folder_Browser.Folder
                {
                    Name = "Arch",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Architects], archcorpDirectories[ArchcorpDirectoryGroups.Architectural_CAD], archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                ongoing.NestedFolders.Add(o_arch);

                #region Project/DWG/Ongoing/Arch
                var o_arch_3D = new Folder_Browser.Folder
                {
                    Name = "3D",
                };
                o_arch.NestedFolders.Add(o_arch_3D);

                var o_arch_cad = new Folder_Browser.Folder
                {
                    Name = "CAD",
                };
                o_arch.NestedFolders.Add(o_arch_cad);

                #region Project/DWG/Ongoingn/Arch/CAD
                var o_arch_dwgregister = new Folder_Browser.Folder
                {
                    Name = "DWG Register",
                };
                o_arch.NestedFolders.Add(o_arch_dwgregister);
                #endregion

                var o_arch_presentation = new Folder_Browser.Folder
                {
                    Name = "Presentation",
                };
                o_arch.NestedFolders.Add(o_arch_presentation);

                var o_arch_revit = new Folder_Browser.Folder
                {
                    Name = "Revit",
                };
                o_arch.NestedFolders.Add(o_arch_revit);

                #region Project/DWG/Ongoing/Arch/Revit

                var o_arch_revit_families = new Folder_Browser.Folder
                {
                    Name = "Families",
                };
                o_arch_revit.NestedFolders.Add(o_arch_revit_families);
                #endregion

                var o_arch_sketches = new Folder_Browser.Folder
                {
                    Name = "Sketches",
                };
                o_arch.NestedFolders.Add(o_arch_sketches);

                var o_arch_xref = new Folder_Browser.Folder
                {
                    Name = "Xref",
                };
                o_arch.NestedFolders.Add(o_arch_xref);


                #endregion

                var o_intr = new Folder_Browser.Folder
                {
                    Name = "Intr",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Intr], archcorpDirectories[ArchcorpDirectoryGroups.Architects], archcorpDirectories[ArchcorpDirectoryGroups.Architectural_CAD], archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                ongoing.NestedFolders.Add(o_intr);


                #region Project/DWG/Ongoing/Intr
                var o_intr_3D = new Folder_Browser.Folder
                {
                    Name = "3D",
                };
                o_intr.NestedFolders.Add(o_intr_3D);

                var o_intr_cad = new Folder_Browser.Folder
                {
                    Name = "CAD",
                };
                o_intr.NestedFolders.Add(o_intr_cad);

                #region Project/DWG/Ongoingn/Intr/CAD
                var o_intr_dwgregister = new Folder_Browser.Folder
                {
                    Name = "DWG Register",
                };
                o_intr_cad.NestedFolders.Add(o_intr_dwgregister);

                var o_intr_superceded = new Folder_Browser.Folder
                {
                    Name = "Superceded",
                };
                o_intr_cad.NestedFolders.Add(o_intr_superceded);
                #endregion

                var o_intr_presentation = new Folder_Browser.Folder
                {
                    Name = "Presentation",
                };
                o_intr_cad.NestedFolders.Add(o_intr_presentation);

                var o_intr_sketches = new Folder_Browser.Folder
                {
                    Name = "Sketches",
                };
                o_intr_cad.NestedFolders.Add(o_intr_sketches);

                var o_intr_xref = new Folder_Browser.Folder
                {
                    Name = "Xref",
                };
                o_intr_cad.NestedFolders.Add(o_intr_xref);
                #endregion


                var o_mep = new Folder_Browser.Folder
                {
                    Name = "MEP",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Mechanical], archcorpDirectories[ArchcorpDirectoryGroups.Mechanical_CAD], archcorpDirectories[ArchcorpDirectoryGroups.Electrical], archcorpDirectories[ArchcorpDirectoryGroups.Electrical_CAD], archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                ongoing.NestedFolders.Add(o_mep);

                #region Project/DWG/Ongoing/MEP
                var o_mep_cad = new Folder_Browser.Folder
                {
                    Name = "CAD",
                };
                o_mep.NestedFolders.Add(o_mep_cad);


                #region Project/DWG/Ongoingn/MEP/CAD
                var o_mep_dwgregister = new Folder_Browser.Folder
                {
                    Name = "DWG Register",
                };
                o_mep_cad.NestedFolders.Add(o_mep_dwgregister);

                #endregion

                var o_mep_revit = new Folder_Browser.Folder
                {
                    Name = "Revit",
                };
                o_mep.NestedFolders.Add(o_mep_revit);

                #region Project/DWG/Ongoingn/MEP/Revit
                var o_mep_revit_families = new Folder_Browser.Folder
                {
                    Name = "Families",
                };
                o_mep_revit.NestedFolders.Add(o_mep_revit_families);

                #endregion

                var o_mep_xref = new Folder_Browser.Folder
                {
                    Name = "Xref",
                };
                o_mep.NestedFolders.Add(o_mep_xref);

                #region Project/DWG/Ongoingn/MEP/Xref
                var o_mep_xref_electrical = new Folder_Browser.Folder
                {
                    Name = "Electrical",
                };
                o_mep_xref.NestedFolders.Add(o_mep_xref_electrical);
                #endregion



                #endregion

                var o_str = new Folder_Browser.Folder
                {
                    Name = "Str",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Structures], archcorpDirectories[ArchcorpDirectoryGroups.Structural_CAD], archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                ongoing.NestedFolders.Add(o_str);

                #endregion

                #endregion

                #region Level 4

                #region Level 4 - Project/Docs/Technical/Project Docs

                var affectionPlan = new Folder_Browser.Folder
                {
                    Name = "Affection Plan",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(affectionPlan);

                var appointmentletters = new Folder_Browser.Folder
                {
                    Name = "Appointment Letters",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(appointmentletters);

                var brief = new Folder_Browser.Folder
                {
                    Name = "Brief",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(brief);

                var clientdocs = new Folder_Browser.Folder
                {
                    Name = "Client Docs",
                    ListAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(clientdocs);

                var communicationmatrix = new Folder_Browser.Folder
                {
                    Name = "Communication Matrix",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(communicationmatrix);

                var feedback = new Folder_Browser.Folder
                {
                    Name = "Feedback",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(feedback);

                var lessonlearnt = new Folder_Browser.Folder
                {
                    Name = "Lesson Learnt",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(lessonlearnt);

                var noc = new Folder_Browser.Folder
                {
                    Name = "NOC",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(noc);

                #region Project/Docs/Technical/Project Docs/NOC
                var noc_register = new Folder_Browser.Folder
                {
                    Name = "NOC Register",
                };
                noc.NestedFolders.Add(noc_register);
                #endregion

                var phaseapproval = new Folder_Browser.Folder
                {
                    Name = "Phase Approval",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(phaseapproval);

                var ram = new Folder_Browser.Folder
                {
                    Name = "RAM",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(ram);

                var schedule = new Folder_Browser.Folder
                {
                    Name = "Schedule",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(schedule);

                var scope = new Folder_Browser.Folder
                {
                    Name = "Scope",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(scope);

                var sitestudies = new Folder_Browser.Folder
                {
                    Name = "Site Studies",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(sitestudies);

                #region Project/Docs/Technical/Project Docs/Site Studies
                var sitestudies_asbuilt = new Folder_Browser.Folder
                {
                    Name = "As Built",
                };
                sitestudies.NestedFolders.Add(sitestudies_asbuilt);

                var sitestudies_images = new Folder_Browser.Folder
                {
                    Name = "Images",
                };
                sitestudies.NestedFolders.Add(sitestudies_images);

                var sitestudies_soilinvestigation = new Folder_Browser.Folder
                {
                    Name = "Soil Investigation",
                };
                sitestudies.NestedFolders.Add(sitestudies_soilinvestigation);

                var sitestudies_survey = new Folder_Browser.Folder
                {
                    Name = "Survey",
                };
                sitestudies.NestedFolders.Add(sitestudies_survey);

                #endregion

                var tradelicense = new Folder_Browser.Folder
                {
                    Name = "Trade License",
                    ReadonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.Dubai_Office_Staff] },
                    ModifyonlyAccessGroup = new List<string> { archcorpDirectories[ArchcorpDirectoryGroups.PMs] },
                };
                projectdocs.NestedFolders.Add(tradelicense);

                #endregion
                #endregion

                return projectFolder;
            }
        }
        public static class Folder
        {
            public static string GetSelectedFolderDirectory()
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        return dialog.SelectedPath;
                    }
                }

                return string.Empty;
            }

            public static void GenerateFolderStructure(Folder_Browser.Folder folderstructure, string directory)
            {

                GenerateFoldersRecursively(new List<Folder_Browser.Folder> { folderstructure }, directory);

                System.Windows.MessageBox.Show("Folder structure has been created succussfully", "Succussful", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);


                //directory
            }
            public static void AddUsersAndPermissions(string DirectoryName, string groupname, FileSystemRights UserRights, AccessControlType AccessType)
            {
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(DirectoryName);
                    DirectorySecurity dirSecurity = directoryInfo.GetAccessControl();
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, groupname);
                    string sid = group.Sid.ToString();
                    SecurityIdentifier secIdentifierSid = new SecurityIdentifier(sid);
                    dirSecurity.AddAccessRule(new FileSystemAccessRule(secIdentifierSid, UserRights, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessType));
                    directoryInfo.SetAccessControl(dirSecurity);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
            private static void GenerateFolder(string directoryPath, Folder_Browser.Folder folder)
            {
                if (System.IO.Directory.Exists(directoryPath))
                {
                    var newFolderPath = $"{directoryPath}\\{folder.Name}";
                    System.IO.Directory.CreateDirectory(newFolderPath);


                    foreach (var groupwith_accesspermission in folder.ListAccessGroup)
                    {
                        AddUsersAndPermissions(newFolderPath, groupwith_accesspermission, FileSystemRights.ListDirectory, AccessControlType.Allow);
                    }

                    foreach (var groupwith_readonlypermission in folder.ReadonlyAccessGroup)
                    {
                        AddUsersAndPermissions(newFolderPath, groupwith_readonlypermission, FileSystemRights.ReadAndExecute, AccessControlType.Allow);
                    }

                    foreach (var groupwith_modifypermission in folder.ModifyonlyAccessGroup)
                    {
                        AddUsersAndPermissions(newFolderPath, groupwith_modifypermission, FileSystemRights.Modify, AccessControlType.Allow);
                    }



                }
            }
            private static void GenerateFoldersRecursively(List<Folder_Browser.Folder> folders, string directory)
            {
                if (folders.Any())
                {
                    foreach (var folder in folders)
                    {
                        Util.Folder.GenerateFolder(directory, folder);
                    }
                    foreach (var folder in folders)
                    {
                        var subfolders = folder.NestedFolders;
                        var subdirectory = directory + "\\" + folder.Name;
                        GenerateFoldersRecursively(subfolders, subdirectory);
                    }

                }
            }



        }

        public static class Parser
        {
            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T">Type of the object to parse</typeparam>
            /// <param name="obj">Object to be parsed</param>
            /// <returns>returns JSON string</returns>
            public static string ParseObject<T>(T obj)
            {
                return new JavaScriptSerializer().Serialize(obj);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T">Type to Convert</typeparam>
            /// <param name="json"></param>
            /// <returns>Parsed object</returns>
            public static T ParseJSON<T>(string json)
            {
                return new JavaScriptSerializer().Deserialize<T>(json);
            }



            /// <summary>
            /// Encode to Base 64
            /// </summary>
            /// <param name="text"></param>
            /// <param name="encoding"></param>
            /// <returns></returns>
            public static string EncodeBase64(string text, Encoding encoding = null)
            {
                if (text == null) return null;

                encoding = encoding ?? Encoding.UTF8;
                var bytes = encoding.GetBytes(text);
                return Convert.ToBase64String(bytes);
            }

            /// <summary>
            /// Decode from base 64 to string
            /// </summary>
            /// <param name="encodedText"></param>
            /// <param name="encoding"></param>
            /// <returns></returns>
            public static string DecodeBase64(string encodedText, Encoding encoding = null)
            {
                if (encodedText == null) return null;

                encoding = encoding ?? Encoding.UTF8;
                var bytes = Convert.FromBase64String(encodedText);
                return encoding.GetString(bytes);
            }


        }

        public static class DB
        {
            public static GenericUnitOfWork UOW = new GenericUnitOfWork();
            //  public static List<FolderTemplate> Templates { get; set; }
            //public static List<FolderTemplate> GetAllFolderTemplatesDB()
            //{
            //    try
            //    {
            //        return UOW.RepositoryPM<FolderTemplate>().SelectAllQueryable().ToList();
            //    }
            //    catch (Exception)
            //    {
            //        return null;
            //    }
            //}
            //public static List<Template_VM> GetAllTemplates_VM(List<FolderTemplate> foldertemps)
            //{
            //    List<Template_VM> templates = new List<Template_VM>();

            //    foreach (var foldertemp in foldertemps)
            //    {
            //        var template = Util.Parser.ParseJSON<Template_VM>(foldertemp.JSON_Template);
            //        templates.Add(template);
            //    }

            //    return templates;
            //}
            //public static List<Template_VM> GetAllTemplates()
            //{
            //    var foldertemps = GetAllFolderTemplatesDB();
            //    List<Template_VM> templates = new List<Template_VM>();

            //    foreach (var foldertemp in foldertemps)
            //    {
            //        var template = Util.Parser.ParseJSON<Template_VM>(foldertemp.JSON_Template);
            //        templates.Add(template);
            //    }

            //    return templates;
            //}

            //public static void InsertTemplate(Template_VM template)
            //{
            //    template.NameTemplate = template.NameTemplate;
            //    var jsonTemplate = Util.Parser.ParseObject<Template_VM>(template);

            //    InsertTemplateDB(template.NameTemplate, jsonTemplate);

            //}
            //private static void InsertTemplateDB(string name_template, string json_folderstructure)
            //{
            //    var folderTemplate = new FolderTemplate { Name_Template = name_template, JSON_Template = json_folderstructure };

            //    UOW.RepositoryPM<FolderTemplate>().Insert(folderTemplate);
            //    UOW.SaveChanges_PM();
            //    Data.RefreshData();
            //}

            public static void DeleteTemplate(Template_VM template)
            {
                //var folderTemplate = Data.FolderTemplates.FirstOrDefault(_ => _.Equals(template.NameTemplate));

                //if (folderTemplate != null)
                //{
                //    folderTemplate.JSON_Template = Util.Parser.ParseObject<Template_VM>(template);
                //    DeleteTemplateDB(folderTemplate);
                //}

            }

            //private static void DeleteTemplateDB(FolderTemplate folderTemplate)
            //{
            //    UOW.RepositoryPM<FolderTemplate>().DeletePM(folderTemplate);
            //    UOW.SaveChanges_PM();
            //    Data.RefreshData();
            //}

            public static void UpdateTemplate(Template_VM template)
            {
                //var folderTemplate = Data.FolderTemplates.FirstOrDefault(_ => _.Equals(template.NameTemplate));

                //if (folderTemplate != null)
                //{
                //    folderTemplate.JSON_Template = Util.Parser.ParseObject<Template_VM>(template);
                //    UpdateTemplateDB(folderTemplate);
                //}


            }
            //private static void UpdateTemplateDB(FolderTemplate folderTemplate)
            //{
            //    UOW.RepositoryPM<FolderTemplate>().UpdatePM(folderTemplate);
            //    UOW.SaveChanges_PM();
            //    Data.RefreshData();
            //}

        }

        public static class Common
        {
            public static string GetAllParentHeaders(TreeViewItem item)
            {

                List<string> headers = new List<string>();

                while ((item) != null)
                {
                    headers.Add(item.Header.ToString());
                    item = (GetSelectedTreeViewItemParent(item) as TreeViewItem);
                }

                headers.Reverse();

                string allParentsFromTopToBottom = String.Join("\\", headers);


                return allParentsFromTopToBottom;
            }

            public static ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(item);
                while (!(parent is TreeViewItem || parent is TreeView))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                return parent as ItemsControl;
            }
        }

        public static class Access
        {
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct SHELLEXECUTEINFO
            {
                public int cbSize;
                public uint fMask;
                public IntPtr hwnd;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpVerb;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpFile;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpParameters;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpDirectory;
                public int nShow;
                public IntPtr hInstApp;
                public IntPtr lpIDList;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpClass;
                public IntPtr hkeyClass;
                public uint dwHotKey;
                public IntPtr hIcon;
                public IntPtr hProcess;
            }

            private const int SW_SHOW = 5;
            private const uint SEE_MASK_INVOKEIDLIST = 12;
            public static bool ShowAccessPermissionWindow(string Filename)
            {
                SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
                info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
                info.lpVerb = "properties";
                info.lpFile = Filename;
                info.nShow = SW_SHOW;
                info.fMask = SEE_MASK_INVOKEIDLIST;
                info.lpParameters = "Security";
                return ShellExecuteEx(ref info);
            }




            // Adds an ACL entry on the specified file for the specified account.
            public static void AddFolderSecurity(string folderPath, string account,
                FileSystemRights rights, AccessControlType controlType)
            {

                var directoryInfo = new DirectoryInfo(folderPath);
                var directorySecurity = directoryInfo.GetAccessControl();
                var currentUserIdentity = WindowsIdentity.GetCurrent();


                // Add the FileSystemAccessRule to the security settings.
                directorySecurity.AddAccessRule(new FileSystemAccessRule(account,
                    rights, controlType));

                // Set the new access settings.
                directoryInfo.SetAccessControl(directorySecurity);

            }

            // Removes an ACL entry on the specified file for the specified account.
            public static void RemoveFileSecurity(string fileName, string account,
                FileSystemRights rights, AccessControlType controlType)
            {

                // Get a FileSecurity object that represents the
                // current security settings.
                FileSecurity fSecurity = File.GetAccessControl(fileName);

                // Remove the FileSystemAccessRule from the security settings.
                fSecurity.RemoveAccessRule(new FileSystemAccessRule(account,
                    rights, controlType));

                // Set the new access settings.
                File.SetAccessControl(fileName, fSecurity);

            }

        }

        public static class ElasticRequest
        {

            public static string GET(string query, string index = "directorynew")
            {
                query = HttpUtility.UrlEncode(query) + "&" + HttpUtility.UrlEncode("source_content_type") + "=" + HttpUtility.UrlEncode("application/json");

                byte[] bytes = Encoding.Default.GetBytes(query);
                query = Encoding.UTF8.GetString(bytes);

                string responseStr = "";

                string url = @"http://192.168.6.238:9200/" + index + @"/_search?source=" + query;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "'application/json; charset=utf-8'";
                //request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseStr = reader.ReadToEnd();
                }

                return responseStr;
            }


            public static List<Item> GetDirectoryTree(string directory)
            {
                string query = @"{""query"":{""match"":{""RootPath.keyword"": """ + directory + @"""}}}";

                // string query = @"{""query"": {""bool"": {""must"": [{""match"": { ""RootPath"": """ + directory + @"""}}]}}}";

                var result = GET(query);

                var jo = Newtonsoft.Json.Linq.JObject.Parse(result);
                List<Item> items = null;
                try
                {

                    items = Util.Parser.ParseJSON<List<Item>>(JsonConvert.SerializeObject(jo["hits"]["hits"][0]["_source"]["Items"]));

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Folder not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Item>();
                }
                items.ForEach(_ =>
                {
                    _.Path = directory;
                    _._rootpath = jo["hits"]["hits"][0]["_source"]["RootPath"].ToString();
                    _._index = jo["hits"]["hits"][0]["_index"].ToString();
                    _._id = jo["hits"]["hits"][0]["_id"].ToString();
                    _._type = jo["hits"]["hits"][0]["_type"].ToString();
                });

                return (from item in items
                        orderby item.Name ascending
                        select item)?.ToList();

            }

            public static List<Item> GetSearchResult(string directory, string searchword)
            {

                directory = directory.Replace("\\\\\\\\acdxbfs1\\\\", "acdxbfs1 AND ");
                if (directory.Last().Equals('\\'))
                {
                    directory = directory.Replace("\\\\", "");
                }

                string query = @"{""from"" : 0, ""size"" : 1000, ""query"": { ""bool"": { ""must"": [{""query_string"": {""query"": """ + directory + @""",""analyze_wildcard"": true,""default_field"": ""RootPath"" }},{ ""match"": {""Items.Name"": """ + searchword + @"""}}], ""filter"": [], ""should"": [],""must_not"": [] }}}";



                var result = GET(query);
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);


                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

                dynamic data = Util.Parser.ParseJSON<List<dynamic>>(JsonConvert.SerializeObject(json["hits"]["hits"]));
                List<Item> items = new List<Item>();
                foreach (dynamic obj in data)
                {
                    var itemCollection = Util.Parser.ParseJSON<List<Item>>(Util.Parser.ParseObject(obj["_source"]["Items"]));
                    var path = obj["_source"]["RootPath"];

                    foreach (var item in itemCollection)
                    {
                        if (culture.CompareInfo.IndexOf(item.Name, searchword, System.Globalization.CompareOptions.IgnoreCase) >= 0)
                        {
                            items.Add(new Item
                            {
                                Path = path.Replace(@"\", @"\\"),
                                Name = item.Name,
                                Type = item.Type,


                                _rootpath = obj["_source"]["RootPath"].ToString(),
                                _index = obj["_index"].ToString(),
                                _id = obj["_id"].ToString(),
                                _type = obj["_type"].ToString(),
                            });


                        }
                    }

                }

                return (from item in items
                        orderby item.Name ascending
                        select item).ToList();

                //return items;
            }


            public static List<Item> GetContentSearchResult(string directory, string searchword)
            {

                //directory = directory.Replace("\\\\\\\\acdxbfs1\\\\", "acdxbfs1 AND ");
                if (directory.Last().Equals('\\'))
                {
                    directory = directory.Replace("\\\\", "");
                }

                //  string query = @"{""from"" : 0, ""size"" : 1000, ""query"": { ""bool"": { ""must"": [{""query_string"": {""query"": """ + directory + @""",""analyze_wildcard"": true,""default_field"": ""RootPath"" }},{ ""match"": {""Items.Name"": """ + searchword + @"""}}], ""filter"": [], ""should"": [],""must_not"": [] }}}";

                string query = @"{""from"": 0, ""size"": 1000,""query"": {""bool"" : {""should"": [{ ""match"": { ""Path"":  """ + directory + @""" }},{ ""match"": { ""Sentence"": """ + searchword + @""" }}]}},""highlight"": {""fields"": {""Sentence"": {}}}}";

                var result = GET(query, "content");
                var json = Newtonsoft.Json.Linq.JObject.Parse(result);


                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

                dynamic data = Util.Parser.ParseJSON<List<dynamic>>(JsonConvert.SerializeObject(json["hits"]["hits"]));
                List<Item> items = new List<Item>();

                foreach (dynamic obj in data)
                {
                    items.Add(new Item
                    {
                        PageNo = obj["_source"]["PageNo"].ToString(),
                        Path = obj["_source"]["Path"].ToString(),
                        Name = obj["_source"]["Name"].ToString(),
                        //Sentence = obj["_source"]["Sentence"].ToString(),
                        Sentence = obj["highlight"]["Sentence"][0].ToString(),
                        Type = obj["_source"]["Type"].ToString(),

                        // _rootpath =  obj["_source"]["RootPath"].ToString(),


                        _index = obj["_index"].ToString(),
                        _id = obj["_id"].ToString(),
                        _type = obj["_type"].ToString(),
                    });



                }

                var itemgroup = items.GroupBy(_ => _.Name);
                var uniqueItems = new List<Item>();

                foreach (var item in itemgroup)
                {
                    var parentItem = item.FirstOrDefault();
                    uniqueItems.Add(parentItem);

                    foreach (var subitem in item.ToList())
                    {
                        parentItem.SubItems.Add(subitem);
                    }
                }

                return (from item in uniqueItems
                        orderby item.Name ascending
                        select item).ToList();

            }


            public static bool POST(string json)
            {

                string b64query = Util.Parser.EncodeBase64(json);
                string url = @"http://192.168.6.238:5000/refresh?json=" + b64query;

                string responseStr = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "'application/json; charset=utf-8'";
                //request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseStr = reader.ReadToEnd();
                }

                var result = bool.Parse(responseStr);
                return result;
            }

        }

        public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeTokenHandle()
                : base(true)
            {
            }

            [DllImport("kernel32.dll")]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [SuppressUnmanagedCodeSecurity]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool CloseHandle(IntPtr handle);

            protected override bool ReleaseHandle()
            {
                return CloseHandle(handle);
            }
        }

        /// <summary>
        /// Class to impersonate another user. Requires user, pass and domain/computername
        /// All code run after impersonationuser has been run will run as this user.
        /// Remember to Dispose() afterwards.
        /// </summary>
        public class ImpersonateUser : IDisposable
        {

            private WindowsImpersonationContext LastContext = null;
            private IntPtr LastUserHandle = IntPtr.Zero;

            #region User Impersonation api
            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);

            [DllImport("advapi32.dll", SetLastError = true)]
            public static extern bool ImpersonateLoggedOnUser(int Token);

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool DuplicateToken(IntPtr token, int impersonationLevel, ref IntPtr duplication);

            [DllImport("kernel32.dll")]
            public static extern Boolean CloseHandle(IntPtr hObject);

            public const int LOGON32_PROVIDER_DEFAULT = 0;
            public const int LOGON32_PROVIDER_WINNT35 = 1;
            public const int LOGON32_LOGON_INTERACTIVE = 2;
            public const int LOGON32_LOGON_NETWORK = 3;
            public const int LOGON32_LOGON_BATCH = 4;
            public const int LOGON32_LOGON_SERVICE = 5;
            public const int LOGON32_LOGON_UNLOCK = 7;
            public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;// Win2K or higher
            public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;// Win2K or higher
            #endregion

            public ImpersonateUser(string username, string domainOrComputerName, string password, int nm = LOGON32_LOGON_NETWORK)
            {

                IntPtr userToken = IntPtr.Zero;
                IntPtr userTokenDuplication = IntPtr.Zero;

                bool loggedOn = false;

                if (domainOrComputerName == null) domainOrComputerName = Environment.UserDomainName;

                if (domainOrComputerName.ToLower() == "nt authority")
                {
                    loggedOn = LogonUser(username, domainOrComputerName, password, LOGON32_LOGON_SERVICE, LOGON32_PROVIDER_DEFAULT, out userToken);
                }
                else
                {
                    loggedOn = LogonUser(username, domainOrComputerName, password, nm, LOGON32_PROVIDER_DEFAULT, out userToken);
                }

                WindowsImpersonationContext _impersonationContext = null;
                if (loggedOn)
                {
                    try
                    {
                        // Create a duplication of the usertoken, this is a solution
                        // for the known bug that is published under KB article Q319615.
                        if (DuplicateToken(userToken, 2, ref userTokenDuplication))
                        {
                            // Create windows identity from the token and impersonate the user.
                            WindowsIdentity identity = new WindowsIdentity(userTokenDuplication);
                            _impersonationContext = identity.Impersonate();
                        }
                        else
                        {
                            // Token duplication failed!
                            // Use the default ctor overload
                            // that will use Mashal.GetLastWin32Error();
                            // to create the exceptions details.
                            throw new System.ComponentModel.Win32Exception();
                        }
                    }
                    finally
                    {
                        // Close usertoken handle duplication when created.
                        if (!userTokenDuplication.Equals(IntPtr.Zero))
                        {
                            // Closes the handle of the user.
                            CloseHandle(userTokenDuplication);
                            userTokenDuplication = IntPtr.Zero;
                        }

                        // Close usertoken handle when created.
                        if (!userToken.Equals(IntPtr.Zero))
                        {
                            // Closes the handle of the user.
                            CloseHandle(userToken);
                            userToken = IntPtr.Zero;
                        }
                    }
                }
                else
                {
                    // Logon failed!
                    // Use the default ctor overload that 
                    // will use Mashal.GetLastWin32Error();
                    // to create the exceptions details.
                    throw new System.ComponentModel.Win32Exception();
                }

                if (LastContext == null) LastContext = _impersonationContext;
            }

            public void Dispose()
            {
                LastContext.Undo();
                LastContext.Dispose();
            }
        }




    }
}
