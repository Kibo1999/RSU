using Newtonsoft.Json;
using PerEncDec.IVI;
using System.Collections;
using System.Net.Http.Headers;
using RSU.JSONIvim;
using PerEncDec.IVI.IVIModule;
using System.ComponentModel;

namespace RSU
{
    public class Json2PerBitAdapter
    {

        public byte[] Json2Bit(Ivim deserializedJson)
        {

            var countryCodeBits = new BitArray(10);
            countryCodeBits.Set(0, false);
            countryCodeBits.Set(1, false);
            countryCodeBits.Set(2, false);
            countryCodeBits.Set(3, false);
            countryCodeBits.Set(4, false);
            countryCodeBits.Set(5, false);
            countryCodeBits.Set(6, false);
            countryCodeBits.Set(7, false);
            countryCodeBits.Set(8, false);
            countryCodeBits.Set(9, false);


            PerEncDec.IVI.IVIMPDUDescriptions.IVIM rootIVI = new PerEncDec.IVI.IVIMPDUDescriptions.IVIM();
            //Header
            rootIVI.Header = new PerEncDec.IVI.ITSContainer.ItsPduHeader();
            rootIVI.Header.MessageID = deserializedJson.header.messageID;
            rootIVI.Header.StationID = deserializedJson.header.stationID;
            rootIVI.Header.ProtocolVersion = deserializedJson.header.protocolVersion;

            //Mandatory
            rootIVI.IviField = new PerEncDec.IVI.IVIModule.IviStructure();
            rootIVI.IviField.Mandatory = new PerEncDec.IVI.IVIModule.IviManagementContainer();
            rootIVI.IviField.Mandatory.ServiceProviderId = new PerEncDec.IVI.EfcDsrcApplication.Provider();
            rootIVI.IviField.Mandatory.ServiceProviderId.CountryCode = new PerEncDec.Asn1.BitString(countryCodeBits);
            rootIVI.IviField.Mandatory.ServiceProviderId.ProviderIdentifier = deserializedJson.ivi.mandatory.serviceProviderId.providerIdentifier;
            rootIVI.IviField.Mandatory.IviIdentificationNumber = deserializedJson.ivi.mandatory.iviIdentificationNumber;
            rootIVI.IviField.Mandatory.TimeStamp = deserializedJson.ivi.mandatory.timeStamp;
            rootIVI.IviField.Mandatory.ValidFrom = deserializedJson.ivi.mandatory.validFrom;
            rootIVI.IviField.Mandatory.ValidTo = deserializedJson.ivi.mandatory.validTo;

            if (deserializedJson.ivi.mandatory.connectedIviStructures is not null) {
                rootIVI.IviField.Mandatory.ConnectedIviStructures.Capacity = (int)deserializedJson.ivi.mandatory.connectedIviStructures;
            }
            rootIVI.IviField.Mandatory.IviStatus = (int)deserializedJson.ivi.mandatory.iviStatus;

            //Optional
            rootIVI.IviField.Optional = new PerEncDec.IVI.IVIModule.IviContainers
            {
                new PerEncDec.IVI.IVIModule.IviContainer(),new PerEncDec.IVI.IVIModule.IviContainer(),new PerEncDec.IVI.IVIModule.IviContainer()
            };

            //Glc
            rootIVI.IviField.Optional[0].Glc = new PerEncDec.IVI.IVIModule.GeographicLocationContainer();
            rootIVI.IviField.Optional[0].Glc.ReferencePosition = new PerEncDec.IVI.ITSContainer.ReferencePosition();
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.Latitude = (long) (deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.lat);
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.Longitude = (long) (deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.lng);
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse = new PerEncDec.IVI.ITSContainer.PosConfidenceEllipse();
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse.SemiMajorConfidence = deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.positionConfidenceElipse.semiMajorConfidence;
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse.SemiMinorConfidence = deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.positionConfidenceElipse.semiMinorConfidence;
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.PositionConfidenceEllipse.SemiMajorOrientation = deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.positionConfidenceElipse.semiMajorOrientation;
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.Altitude = new PerEncDec.IVI.ITSContainer.Altitude();
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.Altitude.AltitudeValue = deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.altitude.altitudeValue;
            rootIVI.IviField.Optional[0].Glc.ReferencePosition.Altitude.AltitudeConfidence = (PerEncDec.IVI.ITSContainer.AltitudeConfidence)deserializedJson.ivi.optional[0].IviContainer.glc.referencePosition.altitude.altitudeConfidence;
            rootIVI.IviField.Optional[0].Glc.ReferencePositionTime = (long?)deserializedJson.ivi.optional[0].IviContainer.glc.referencePositionTime;

            //Glc//Parts
            rootIVI.IviField.Optional[0].Glc.Parts = new PerEncDec.IVI.IVIModule.GlcParts();
            foreach (JSONIvim.GlcPart glcPart in deserializedJson.ivi.optional[0].IviContainer.glc.parts.GlcPart)
            {
                PerEncDec.IVI.IVIModule.GlcPart newPart = new PerEncDec.IVI.IVIModule.GlcPart();
                newPart.ZoneId = glcPart.zoneId;
                newPart.LaneNumber = (long?)glcPart.laneNumber;
                newPart.ZoneExtension = (int?)glcPart.zoneExtension;
                newPart.ZoneHeading = (int?)glcPart.zoneHeading;
                newPart.Zone = new PerEncDec.IVI.IVIModule.Zone();
                newPart.Zone.Segment = new PerEncDec.IVI.IVIModule.Segment();
                newPart.Zone.Segment.Line = new PerEncDec.IVI.IVIModule.PolygonalLine();

                //DeltaPosition
                if (glcPart.zone.segment.line.deltaPositions is not null) { 
                    newPart.Zone.Segment.Line.DeltaPositions = new PerEncDec.IVI.IVIModule.DeltaPositions();
                    foreach (JSONIvim.DeltaPosition pairCoordenates in glcPart.zone.segment.line.deltaPositions.DeltaPosition)
                    {
                        PerEncDec.IVI.IVIModule.DeltaPosition? newCoord = new PerEncDec.IVI.IVIModule.DeltaPosition();
                        newCoord.DeltaLatitude = (long) pairCoordenates.deltaLatitude;
                        newCoord.DeltaLongitude = (long) pairCoordenates.deltaLongitude;
                        newPart.Zone.Segment.Line.DeltaPositions.Add(newCoord);
                    }
                }
                //AbsolutePosition
                else if (glcPart.zone.segment.line.absolutePositions is not null)
                {
                    newPart.Zone.Segment.Line.AbsolutePositions = new PerEncDec.IVI.IVIModule.AbsolutePositions();
                    foreach (JSONIvim.AbsolutePosition pairCoordenates in glcPart.zone.segment.line.absolutePositions.AbsolutePosition)
                    {
                        PerEncDec.IVI.IVIModule.AbsolutePosition? newCoord = new PerEncDec.IVI.IVIModule.AbsolutePosition();
                        newCoord.Latitude = (long) (pairCoordenates.latitude);
                        newCoord.Longitude = (long) (pairCoordenates.longitude);
                        newPart.Zone.Segment.Line.AbsolutePositions.Add(newCoord);
                    }
                }

                newPart.Zone.Segment.LaneWidth = (int?)glcPart.zone.segment.laneWidth;
                    
                rootIVI.IviField.Optional[0].Glc.Parts.Add(newPart);
            }

            //Giv
            rootIVI.IviField.Optional[1].Giv = new PerEncDec.IVI.IVIModule.GeneralIviContainer
            {
                new PerEncDec.IVI.IVIModule.GicPart()
            };

            rootIVI.IviField.Optional[1].Giv[0].DetectionZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
            foreach (DetectionZoneId detectionZoneId in deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.detectionZoneIds)
            {
                rootIVI.IviField.Optional[1].Giv[0].DetectionZoneIds.Add(detectionZoneId.Zid);
            }

            rootIVI.IviField.Optional[1].Giv[0].RelevanceZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
            foreach (RelevanceZoneId relevanceZoneId in deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.relevanceZoneIds)
            {
                rootIVI.IviField.Optional[1].Giv[0].RelevanceZoneIds.Add(relevanceZoneId.Zid);
            }

            rootIVI.IviField.Optional[1].Giv[0].Direction = (int?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.direction;
            rootIVI.IviField.Optional[1].Giv[0].MinimumAwarenessTime = (int?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.minimumAwarenessTime;
            rootIVI.IviField.Optional[1].Giv[0].IviType = (int)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.iviType;
            rootIVI.IviField.Optional[1].Giv[0].IviPurpose = (int?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.iviPurpose;
            rootIVI.IviField.Optional[1].Giv[0].LaneStatus = (long?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.laneStatus;
            rootIVI.IviField.Optional[1].Giv[0].DriverCharacteristics = (int?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.driverCharacteristics;
            rootIVI.IviField.Optional[1].Giv[0].LayoutId = (long?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.layoutId;
            rootIVI.IviField.Optional[1].Giv[0].PreStoredlayoutId = (long?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.preStoredlayoutId;
            rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes = new PerEncDec.IVI.IVIModule.RoadSignCodes
            {
                new PerEncDec.IVI.IVIModule.RSCode()
            };
            rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].LayoutComponentId = (long?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.layoutComponentId;

            //MessageBox.Show(deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.serviceCategoryCode.trafficSignPictogram.ToString());
                
            //Giv//ViennaConvention
            if (deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention is not null)
            {
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code = new PerEncDec.IVI.IVIModule.RSCode.CodeType();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention = new VcCode();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.RoadSignClass = (int)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.roadSignClass;
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.RoadSignCode = (int)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.roadSignCode;
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.VcOption = (int)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.vcOption;
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ViennaConvention.Value = (int?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.viennaConvention.value;
            }
            //Giv//Iso14823
            else if (deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823 is not null)
            {
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code = new PerEncDec.IVI.IVIModule.RSCode.CodeType();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823  = new ISO14823Code();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode = new ISO14823Code.PictogramCodeType();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.CountryCode = null;  //new PerEncDec.Asn1.BitString(countryCodeBits);
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.ServiceCategoryCode = new ISO14823Code.PictogramCodeType.ServiceCategoryCodeType();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.ServiceCategoryCode.TrafficSignPictogram = (ISO14823Code.PictogramCodeType.ServiceCategoryCodeType.TrafficSignPictogramType?)deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.serviceCategoryCode.trafficSignPictogram;
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.PictogramCategoryCode = new ISO14823Code.PictogramCodeType.PictogramCategoryCodeType();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.PictogramCategoryCode.Nature = deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.pictogramCategoryCode.nature;
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.PictogramCode.PictogramCategoryCode.SerialNumber = deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.pictogramCode.pictogramCategoryCode.serialNumber;

                if (deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.attributes is not null)
                {
                    rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.Attributes = new ISO14823Attributes();

                    foreach (JSONIvim.Attribute attr in deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.iso14823.attributes)
                    {
                        //DTM
                        if (attr.DTM is not null)
                        {
                            PerEncDec.IVI.IVIModule.ISO14823Attribute dtmToAdd = new PerEncDec.IVI.IVIModule.ISO14823Attribute();
                            dtmToAdd.Dtm = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod();

                            if (attr.DTM.year.yearRangeStartYear is not null && attr.DTM.year.yearRangeEndYear is not null)
                            {
                                dtmToAdd.Dtm.Year = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod.YearType();
                                dtmToAdd.Dtm.Year.YearRangeStartYear = (long)attr.DTM.year.yearRangeStartYear;
                                dtmToAdd.Dtm.Year.YearRangeEndYear = (long)attr.DTM.year.yearRangeEndYear;
                            }

                            if (attr.DTM.month_day.dateRangeStartMonthDay.month is not null && attr.DTM.month_day.dateRangeEndMonthDay.month is not null 
                                && attr.DTM.month_day.dateRangeStartMonthDay.day is not null && attr.DTM.month_day.dateRangeEndMonthDay.day is not null)
                            {
                                dtmToAdd.Dtm.MonthDay = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod.MonthDayType();
                                dtmToAdd.Dtm.MonthDay.DateRangeStartMonthDay = new PerEncDec.IVI.GDD.MonthDay();
                                dtmToAdd.Dtm.MonthDay.DateRangeStartMonthDay.Month = (int)(long)attr.DTM.month_day.dateRangeStartMonthDay.month;
                                dtmToAdd.Dtm.MonthDay.DateRangeStartMonthDay.Day = (int)(long)attr.DTM.month_day.dateRangeStartMonthDay.day;
                                dtmToAdd.Dtm.MonthDay.DateRangeEndMonthDay = new PerEncDec.IVI.GDD.MonthDay();
                                dtmToAdd.Dtm.MonthDay.DateRangeEndMonthDay.Month = (int)(long)attr.DTM.month_day.dateRangeEndMonthDay.month;
                                dtmToAdd.Dtm.MonthDay.DateRangeEndMonthDay.Day = (int)(long)attr.DTM.month_day.dateRangeEndMonthDay.day;
                            }

                            if (attr.DTM.repeatingPeriodDayTypes is not null)
                            {
                                dtmToAdd.Dtm.RepeatingPeriodDayTypes = null; //attr.DTM.repeatingPeriodDayTypes;
                            }

                            if (attr.DTM.hourMinutes.timeRangeStartTime.hours is not null && attr.DTM.hourMinutes.timeRangeStartTime.mins is not null
                                && attr.DTM.hourMinutes.timeRangeEndTime.hours is not null && attr.DTM.hourMinutes.timeRangeEndTime.mins is not null)
                            {
                                dtmToAdd.Dtm.HourMinutes = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod.HourMinutesType();
                                dtmToAdd.Dtm.HourMinutes.TimeRangeStartTime = new PerEncDec.IVI.GDD.HoursMinutes();
                                dtmToAdd.Dtm.HourMinutes.TimeRangeStartTime.Hours = (int)(long)attr.DTM.hourMinutes.timeRangeStartTime.hours;
                                dtmToAdd.Dtm.HourMinutes.TimeRangeStartTime.Mins = (int)(long)attr.DTM.hourMinutes.timeRangeStartTime.mins;
                                dtmToAdd.Dtm.HourMinutes.TimeRangeEndTime = new PerEncDec.IVI.GDD.HoursMinutes();
                                dtmToAdd.Dtm.HourMinutes.TimeRangeEndTime.Hours = (int)(long)attr.DTM.hourMinutes.timeRangeEndTime.hours;
                                dtmToAdd.Dtm.HourMinutes.TimeRangeEndTime.Mins = (int)(long)attr.DTM.hourMinutes.timeRangeEndTime.mins;
                            }

                            if (attr.DTM.dateRangeOfWeek is not null)
                            {
                                dtmToAdd.Dtm.DateRangeOfWeek = null;  //attr.DTM.dateRangeOfWeek;
                            }

                            if (attr.DTM.durationHourMinute.hours is not null && attr.DTM.durationHourMinute.mins is not null)
                            {
                                dtmToAdd.Dtm.DurationHourMinute = new PerEncDec.IVI.GDD.HoursMinutes();
                                dtmToAdd.Dtm.DurationHourMinute.Hours = (int)(long)attr.DTM.durationHourMinute.hours;
                                dtmToAdd.Dtm.DurationHourMinute.Mins = (int)(long)attr.DTM.durationHourMinute.mins;
                            }

                            rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.Attributes.Add(dtmToAdd);
                        }
                        //EDT
                        else if (attr.EDT is not null)
                        {
                            PerEncDec.IVI.IVIModule.ISO14823Attribute edtToAdd = new PerEncDec.IVI.IVIModule.ISO14823Attribute();
                            edtToAdd.Edt = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod();


                            if (attr.EDT.year.yearRangeStartYear is not null && attr.EDT.year.yearRangeEndYear is not null)
                            {
                                edtToAdd.Edt.Year = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod.YearType();
                                edtToAdd.Edt.Year.YearRangeStartYear = (int)(long)attr.EDT.year.yearRangeStartYear;
                                edtToAdd.Edt.Year.YearRangeEndYear = (int)(long)attr.EDT.year.yearRangeEndYear;
                            }

                            if (attr.EDT.month_day.dateRangeStartMonthDay.month is not null && attr.EDT.month_day.dateRangeEndMonthDay.month is not null
                                && attr.EDT.month_day.dateRangeStartMonthDay.day is not null && attr.EDT.month_day.dateRangeEndMonthDay.day is not null)
                            {
                                edtToAdd.Edt.MonthDay = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod.MonthDayType();
                                edtToAdd.Edt.MonthDay.DateRangeStartMonthDay = new PerEncDec.IVI.GDD.MonthDay();
                                edtToAdd.Edt.MonthDay.DateRangeStartMonthDay.Month = (int)(long)attr.EDT.month_day.dateRangeStartMonthDay.month;
                                edtToAdd.Edt.MonthDay.DateRangeStartMonthDay.Day = (int)(long)attr.EDT.month_day.dateRangeStartMonthDay.day;
                                edtToAdd.Edt.MonthDay.DateRangeEndMonthDay = new PerEncDec.IVI.GDD.MonthDay();
                                edtToAdd.Edt.MonthDay.DateRangeEndMonthDay.Month = (int)(long)attr.EDT.month_day.dateRangeEndMonthDay.month;
                                edtToAdd.Edt.MonthDay.DateRangeEndMonthDay.Day = (int)(long)attr.EDT.month_day.dateRangeEndMonthDay.day;
                            }

                            if (attr.EDT.repeatingPeriodDayTypes is not null)
                            {
                                edtToAdd.Edt.RepeatingPeriodDayTypes = null; //attr.DTM.repeatingPeriodDayTypes;
                            }

                            if (attr.EDT.hourMinutes.timeRangeStartTime.hours is not null && attr.EDT.hourMinutes.timeRangeStartTime.mins is not null
                                && attr.EDT.hourMinutes.timeRangeEndTime.hours is not null && attr.EDT.hourMinutes.timeRangeEndTime.mins is not null)
                            {
                                edtToAdd.Edt.HourMinutes = new PerEncDec.IVI.GDD.InternationalSignApplicablePeriod.HourMinutesType();
                                edtToAdd.Edt.HourMinutes.TimeRangeStartTime = new PerEncDec.IVI.GDD.HoursMinutes();
                                edtToAdd.Edt.HourMinutes.TimeRangeStartTime.Hours = (int)(long)attr.EDT.hourMinutes.timeRangeStartTime.hours;
                                edtToAdd.Edt.HourMinutes.TimeRangeStartTime.Mins = (int)(long)attr.EDT.hourMinutes.timeRangeStartTime.mins;
                                edtToAdd.Edt.HourMinutes.TimeRangeEndTime = new PerEncDec.IVI.GDD.HoursMinutes();
                                edtToAdd.Edt.HourMinutes.TimeRangeEndTime.Hours = (int)(long)attr.EDT.hourMinutes.timeRangeEndTime.hours;
                                edtToAdd.Edt.HourMinutes.TimeRangeEndTime.Mins = (int)(long)attr.EDT.hourMinutes.timeRangeEndTime.mins;
                            }

                            if (attr.EDT.dateRangeOfWeek is not null)
                            {
                                edtToAdd.Edt.DateRangeOfWeek = null;  //attr.DTM.dateRangeOfWeek;
                            }

                            if (attr.EDT.durationHourMinute.hours is not null && attr.EDT.durationHourMinute.mins is not null)
                            {
                                edtToAdd.Edt.DurationHourMinute = new PerEncDec.IVI.GDD.HoursMinutes();
                                edtToAdd.Edt.DurationHourMinute.Hours = (int)(long)attr.EDT.durationHourMinute.hours;
                                edtToAdd.Edt.DurationHourMinute.Mins = (int)(long)attr.EDT.durationHourMinute.mins;
                            }

                            rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.Attributes.Add(edtToAdd);
                        }
                        //DFLType
                        else if (String.IsNullOrEmpty(attr.DFLType.ToString()))
                        {
                            PerEncDec.IVI.IVIModule.ISO14823Attribute dflToAdd = new PerEncDec.IVI.IVIModule.ISO14823Attribute();
                            dflToAdd.Dfl = attr.DFLType;

                            rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.Attributes.Add(dflToAdd);
                        }
                        //VED
                        else if (attr.VED is not null)
                        {
                            PerEncDec.IVI.IVIModule.ISO14823Attribute vedToAdd = new PerEncDec.IVI.IVIModule.ISO14823Attribute();
                            vedToAdd.Ved = new PerEncDec.IVI.GDD.InternationalSignApplicableVehicleDimensions();

                            if (attr.VED.vehicleHeight.value is not null)
                            {
                                vedToAdd.Ved.VehicleHeight = new PerEncDec.IVI.GDD.Distance();
                                vedToAdd.Ved.VehicleHeight.Value = (int)attr.VED.vehicleHeight.value;
                                vedToAdd.Ved.VehicleHeight.Unit = (int)attr.VED.vehicleHeight.unit;
                            }

                            if (attr.VED.vehicleWidth.value is not null)
                            {
                                vedToAdd.Ved.VehicleWidth = new PerEncDec.IVI.GDD.Distance();
                                vedToAdd.Ved.VehicleWidth.Value = (int)attr.VED.vehicleWidth.value;
                                vedToAdd.Ved.VehicleWidth.Unit = (int)attr.VED.vehicleWidth.unit;
                            }

                            if (attr.VED.vehicleLength.value is not null)
                            {
                                vedToAdd.Ved.VehicleLength = new PerEncDec.IVI.GDD.Distance();
                                vedToAdd.Ved.VehicleLength.Value = (int)attr.VED.vehicleLength.value;
                                vedToAdd.Ved.VehicleLength.Unit = (int)attr.VED.vehicleLength.unit;
                            }

                            if (attr.VED.vehicleWeight.value is not null)
                            {
                                vedToAdd.Ved.VehicleWeight = new PerEncDec.IVI.GDD.Weight();
                                vedToAdd.Ved.VehicleWeight.Value = (int)attr.VED.vehicleWeight.value;
                                vedToAdd.Ved.VehicleWeight.Unit = (int)attr.VED.vehicleWeight.unit;
                            }

                            rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.Iso14823.Attributes.Add(vedToAdd);
                        }
                    }
                }
            }
            //Giv//ItsCodes
            else if (deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.itisCode is not null)
            {
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code = new PerEncDec.IVI.IVIModule.RSCode.CodeType();
                rootIVI.IviField.Optional[1].Giv[0].RoadSignCodes[0].Code.ItisCodes = deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.roadSignCodes[0].RSCode.code.itisCode;
            }

            rootIVI.IviField.Optional[1].Giv[0].ExtraText = new PerEncDec.IVI.IVIModule.ConstraintTextLines1
            {
                new PerEncDec.IVI.IVIModule.Text()
            };
            rootIVI.IviField.Optional[1].Giv[0].ExtraText[0].Language = new PerEncDec.Asn1.BitString(10);
            rootIVI.IviField.Optional[1].Giv[0].ExtraText[0].TextContent = deserializedJson.ivi.optional[0].IviContainer.giv[0].GicPart.extraText.ToString();


            //Tc
            rootIVI.IviField.Optional[2].Tc = new PerEncDec.IVI.IVIModule.TextContainer
            {                   
                new PerEncDec.IVI.IVIModule.TcPart()
            };

            rootIVI.IviField.Optional[2].Tc[0].DetectionZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
            foreach (DetectionZone detectionZone in deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.DetectionZone)
            {
                rootIVI.IviField.Optional[2].Tc[0].DetectionZoneIds.Add(detectionZone.Zid);
            }

            rootIVI.IviField.Optional[2].Tc[0].RelevanceZoneIds = new PerEncDec.IVI.IVIModule.ZoneIds();
            foreach (RelevanceZone relevanceZone in deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.RelevanceZone)
            {
                rootIVI.IviField.Optional[2].Tc[0].RelevanceZoneIds.Add(relevanceZone.Zid);
            }

            rootIVI.IviField.Optional[2].Tc[0].Direction = (int?)deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.Direction;
            rootIVI.IviField.Optional[2].Tc[0].MinimumAwarenessTime = (int?)deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.minimumAwarenessZoneIds;
            rootIVI.IviField.Optional[2].Tc[0].LayoutId = (long?)deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.layoutId;
            rootIVI.IviField.Optional[2].Tc[0].PreStoredlayoutId = (long?)deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.preStoredlayoutId;
            rootIVI.IviField.Optional[2].Tc[0].Text = new PerEncDec.IVI.IVIModule.TextLines();
            //rootIVI.IviField.Optional[0].Tc[0].Text = deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.text;
            rootIVI.IviField.Optional[2].Tc[0].Data = BitConverter.GetBytes(deserializedJson.ivi.optional[0].IviContainer.tc[0].TcPart.data.Value.Ticks);


            PerUnalignedCodec codec = new PerUnalignedCodec();
            byte[] ivib = codec.Encode(rootIVI);
            return ivib;
            //escreve o ficheiro indentificado pelo ID do primeiro IVI dentro desta mensagem IVIM
            /*File.WriteAllBytes("JSonMessageBin"+ deserializedJson.ivi.mandatory.iviIdentificationNumber +".ivi", ivib);

            PerEncDec.IVI.IVIMPDUDescriptions.IVIM rootIvi2 = new PerEncDec.IVI.IVIMPDUDescriptions.IVIM();
            codec.Decode(ivib, 0, rootIvi2);
            */
            
        }     
    }
}
