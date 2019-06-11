/* 
 * IfA Standards API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Ifa.Api.Model
{
    /// <summary>
    /// ApiSkillDataModel
    /// </summary>
    [DataContract]
    public partial class ApiSkillDataModel :  IEquatable<ApiSkillDataModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiSkillDataModel" /> class.
        /// </summary>
        /// <param name="detail">detail.</param>
        /// <param name="skillId">skillId.</param>
        public ApiSkillDataModel(string detail = default(string), string skillId = default(string))
        {
            this.Detail = detail;
            this.SkillId = skillId;
        }
        
        /// <summary>
        /// Gets or Sets Detail
        /// </summary>
        [DataMember(Name="detail", EmitDefaultValue=false)]
        public string Detail { get; set; }

        /// <summary>
        /// Gets or Sets SkillId
        /// </summary>
        [DataMember(Name="skillId", EmitDefaultValue=false)]
        public string SkillId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApiSkillDataModel {\n");
            sb.Append("  Detail: ").Append(Detail).Append("\n");
            sb.Append("  SkillId: ").Append(SkillId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ApiSkillDataModel);
        }

        /// <summary>
        /// Returns true if ApiSkillDataModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ApiSkillDataModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ApiSkillDataModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Detail == input.Detail ||
                    (this.Detail != null &&
                    this.Detail.Equals(input.Detail))
                ) && 
                (
                    this.SkillId == input.SkillId ||
                    (this.SkillId != null &&
                    this.SkillId.Equals(input.SkillId))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Detail != null)
                    hashCode = hashCode * 59 + this.Detail.GetHashCode();
                if (this.SkillId != null)
                    hashCode = hashCode * 59 + this.SkillId.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
